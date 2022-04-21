using Flunt.Validations;
using MediatR;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.Domain.UseCases.Common.Commands;

public abstract class ApproveDisapproveUserCommand : BaseCommand, IRequest<Response>
{
    public Guid UserId { get; init; }
    
    protected override void Validate()
    {
        AddNotifications(new Contract<bool>()
            .IsFalse(UserId == Guid.Empty, nameof(UserId), "Usuário informado é inválido"));
    }
}