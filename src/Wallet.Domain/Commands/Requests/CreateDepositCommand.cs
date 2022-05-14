using Flunt.Validations;
using MediatR;
using Wallet.Shared.Commands;
using Wallet.Shared.Results;

namespace Wallet.Domain.Commands.Requests;

public class CreateDepositCommand : Command, IRequest<Result>
{
    public Guid DestinationAccountId { get; init; }
    public decimal Amount { get; init; }

    public CreateDepositCommand()
    {
        
    }
    
    public CreateDepositCommand(Guid destinationAccountId, decimal amount)
    {
        DestinationAccountId = destinationAccountId;
        Amount = amount;
    }
    
    protected override void Validate()
    {
        AddNotifications(new Contract<bool>()
            .IsTrue(DestinationAccountId != Guid.Empty, nameof(DestinationAccountId), "Conta de destino é obrigatória")
            .IsGreaterThan(Amount, 0, nameof(Amount), "Valor é obrigatório"));
    }
}
