using System.Data;
using MediatR;
using Wallet.Domain.Commands.Requests;
using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Shared.Handlers;
using Wallet.Shared.Results;

namespace Wallet.Domain.Commands.Handlers;

public class BalanceHandler : Handler, IRequestHandler<AddBalanceByTransactionCommand, Result>
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

    public async Task<Result> Handle(AddBalanceByTransactionCommand command, CancellationToken cancellationToken)
    {
        var result = new Result();
        var transaction = await _transactionRepository.GetAsync(command.TransactionId);
        
        if (transaction is null)
            return result.AddNotification("Transação informada não foi encontrada");

        await ProcessBalance(transaction.SourceAccount, transaction, isDebit: true);
        await ProcessBalance(transaction.DestinationAccount, transaction, isDebit: false);
        
        return result;
    }

    private async Task ProcessBalance(Account? account, Transaction transaction, bool isDebit)
    {
        if (account == null)
            return;

        var balance = new Balance(account, transaction, isDebit, transaction.Amount);
        
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            balance = await _balanceRepository.AddAsync(balance);
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
