using MediatR;
using Wallet.Domain.Commands.Requests;
using Wallet.Domain.Entities;
using Wallet.Domain.Enumerations;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Shared.Handlers;
using Wallet.Shared.Results;

namespace Wallet.Domain.Commands.Handlers;

public class TransactionHandler : Handler, 
    IRequestHandler<CreateTransferCommand, Result>,
    IRequestHandler<CreateDepositCommand, Result>
{
    private readonly ISession _session;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    
    public TransactionHandler(
        ISession session, 
        IMediator mediator, 
        IUnitOfWork unitOfWork, 
        ITransactionRepository transactionRepository, 
        IAccountRepository accountRepository)
    {
        _session = session;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Result> Handle(CreateTransferCommand command, CancellationToken cancellationToken)
    {
        var result = new Result();
        
        var sourceAccount = _session.User.GetAccount(command.SourceAccountId);
        var destinationAccount = await _accountRepository.GetAsync(command.DestinationAccountId);
        
        if (sourceAccount is null)
            return result.AddNotification("A conta de origem informada não pertence ao usuário da sessão");
        
        if(destinationAccount is null)
            return result.AddNotification("A conta de destino informada não foi encontrada");

        if(sourceAccount.Balance < command.Amount)
            return result.AddNotification("O saldo é insuficiente para esta transferência");

        var transaction = await _transactionRepository.AddAsync(new Transaction(sourceAccount, destinationAccount, TransactionType.Transfer, command.Amount));

        await _mediator.Send(new AddBalanceByTransactionCommand(transaction.Id), cancellationToken);
        
        return result;
    }
    
    public async Task<Result> Handle(CreateDepositCommand command, CancellationToken cancellationToken)
    {
        var result = new Result();

        var destinationAccount = await _accountRepository.GetAsync(command.DestinationAccountId);
        
        if(destinationAccount is null)
            return result.AddNotification("Conta de destino informada não foi encontrada");

        var transaction = await _transactionRepository.AddAsync(new Transaction(destinationAccount, TransactionType.Deposit, command.Amount));

        await _mediator.Send(new AddBalanceByTransactionCommand(transaction.Id), cancellationToken);
        
        return result;
    }
}
