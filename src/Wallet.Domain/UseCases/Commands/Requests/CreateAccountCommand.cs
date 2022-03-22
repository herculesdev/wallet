using Flunt.Validations;
using MediatR;
using Wallet.Domain.Enumerations;
using Wallet.Domain.UseCases.Common.Commands;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Commands.Requests;

public class CreateAccountCommand : BaseCommand, IRequest<Response>
{
    public Guid OwnerId { get; set; }
    public LegalNature Type { get; set; }
    
    protected override void Validate()
    {
        AddNotifications(new Contract<bool>()
            .IsTrue(OwnerId != Guid.Empty, nameof(OwnerId), "Identificação do titular é obrigatória"));
    }
}
