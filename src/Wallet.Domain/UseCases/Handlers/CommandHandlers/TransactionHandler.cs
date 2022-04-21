using MediatR;
using Wallet.Domain.Entities;
using Wallet.Domain.Enumerations;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Commands.Requests;
using Wallet.Domain.UseCases.Common.Handlers;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Handlers.CommandHandlers;

public class TransactionHandler : BaseHandler, 
    IRequestHandler<CreateTransferCommand, Response>,
    IRequestHandler<CreateDepositCommand, Response>
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

    public async Task<Response> Handle(CreateTransferCommand command, CancellationToken cancellationToken)
    {
        var response = new Response();
        
        var sourceAccount = _session.User.GetAccount(command.SourceAccountId);
        var destinationAccount = await _accountRepository.GetAsync(command.DestinationAccountId);
        
        if (sourceAccount is null)
            return response.AddNotification("Conta de origem informada não pertence ao usuário da sessão");
        
        if(destinationAccount is null)
            return response.AddNotification("Conta de destino informada não foi encontrada");

        if(sourceAccount.Balance < command.Amount)
            return response.AddNotification("Saldo insuficiente para esta transferência");

        var transaction = await _transactionRepository.AddAsync(new Transaction
        {
            From = sourceAccount,
            To = destinationAccount,
            Type = TransactionType.Transfer,
            Amount = command.Amount
        });
        await _unitOfWork.CommitAsync();

        await _mediator.Send(new AddBalanceByTransactionCommand(transaction.Id), cancellationToken);
        
        return response;
    }
    
    public async Task<Response> Handle(CreateDepositCommand command, CancellationToken cancellationToken)
    {
        var response = new Response();

        var destinationAccount = await _accountRepository.GetAsync(command.DestinationAccountId);
        
        if(destinationAccount is null)
            return response.AddNotification("Conta de destino informada não foi encontrada");

        var transaction = await _transactionRepository.AddAsync(new Transaction
        {
            From = null,
            To = destinationAccount,
            Type = TransactionType.Deposit,
            Amount = command.Amount
        });
        
        await _unitOfWork.CommitAsync();

        await _mediator.Send(new AddBalanceByTransactionCommand(transaction.Id), cancellationToken);
        
        return response;
    }
}