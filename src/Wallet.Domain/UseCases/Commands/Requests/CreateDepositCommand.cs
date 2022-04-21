using Flunt.Validations;
using MediatR;
using Wallet.Domain.UseCases.Common.Commands;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Commands.Requests;

public class CreateDepositCommand : BaseCommand, IRequest<Response>
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