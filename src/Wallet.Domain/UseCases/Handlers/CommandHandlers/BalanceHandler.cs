using MediatR;
using Wallet.Domain.Entities;
using Wallet.Domain.Enumerations;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Commands.Requests;
using Wallet.Domain.UseCases.Common.Handlers;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Handlers.CommandHandlers;

public class BalanceHandler : BaseHandler, IRequestHandler<AddBalanceByTransactionCommand, Response>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBalanceRepository _balanceRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    
    public BalanceHandler(
        IUnitOfWork unitOfWork, 
        IBalanceRepository balanceRepository,
        ITransactionRepository transactionRepository, 
        IAccountRepository accountRepository)
    {
        _unitOfWork = unitOfWork;
        _balanceRepository = balanceRepository;
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Response> Handle(AddBalanceByTransactionCommand command, CancellationToken cancellationToken)
    {
        var response = new Response();

        if (!await _transactionRepository.HasTransactionWith(command.TransactionId))
            return response.AddNotification("Transação informada não foi encontrada");
        
        var transaction = await _transactionRepository.GetById(command.TransactionId);
        var sourceAccount = transaction.From;
        var destinationAccount = transaction.To;
        Balance? sourceDebit = null;
        
        if(sourceAccount != null)
            sourceDebit = await _balanceRepository.Add(new Balance
            {
                Account = sourceAccount!,
                Transaction = transaction,
                IsDebit = true,
                Value = transaction.Amount
            });
        
        var destinationCredit = await _balanceRepository.Add(new Balance
        {
            Account = destinationAccount,
            Transaction = transaction,
            IsDebit = false,
            Value = transaction.Amount
        });
        
        await _unitOfWork.CommitAsync();
        
        if(sourceAccount != null && sourceDebit != null)
        {
            sourceAccount!.Balance -= sourceDebit.Value;
            await _accountRepository.Update(sourceAccount!);
        }

        destinationAccount.Balance += destinationCredit.Value;
        await _accountRepository.Update(destinationAccount);
        
        await _unitOfWork.CommitAsync();
        
        
        return response;
    }
}
