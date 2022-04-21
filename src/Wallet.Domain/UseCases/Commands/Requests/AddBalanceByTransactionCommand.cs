using Flunt.Validations;
using MediatR;
using Wallet.Domain.UseCases.Common.Commands;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Commands.Requests;

public class AddBalanceByTransactionCommand : BaseCommand, IRequest<Response>
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