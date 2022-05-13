using Flunt.Validations;
using MediatR;
using Wallet.Shared.Commands;
using Wallet.Shared.Results;

namespace Wallet.Domain.UseCases.Commands.Requests;

public class AddBalanceByTransactionCommand : Command, IRequest<Result>
{
    public Guid TransactionId { get; set; }

    public AddBalanceByTransactionCommand(Guid transactionId)
    {
        TransactionId = transactionId;
    }
    
    protected override void Validate()
    {
        AddNotifications(new Contract<bool>()
            .IsTrue(TransactionId != Guid.Empty, nameof(TransactionId), "Transação é obrigatória"));
    }
}
