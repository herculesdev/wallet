using System.Data;
using MediatR;
using Wallet.Domain.Entities;
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
        var transaction = await _transactionRepository.GetAsync(command.TransactionId);
        
        if (transaction is null)
            return response.AddNotification("Transação informada não foi encontrada");

        await ProcessBalance(transaction.SourceAccount, transaction, isDebit: true);
        await ProcessBalance(transaction.DestinationAccount, transaction, isDebit: false);
        
        return response;
    }

    private async Task ProcessBalance(Account? account, Transaction transaction, bool isDebit)
    {
        if (account == null)
            return;

        Balance? balance = null;
        
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            balance = await _balanceRepository.AddAsync(new Balance
            {
                Account = account,
                Transaction = transaction,
                IsDebit = isDebit,
                Value = transaction.Amount
            });
            
            account.UpdateBalanceValue(balance);
            await _accountRepository.UpdateAsync(account);
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (DBConcurrencyException e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            // reload from db to get most recently version or clear non-persisted entity
            // to prevent concurrency problems
            await _unitOfWork.ReloadAsync(account);
            await _unitOfWork.ReloadAsync(balance);
            await ProcessBalance(account, transaction, isDebit);
        }
    }
}
