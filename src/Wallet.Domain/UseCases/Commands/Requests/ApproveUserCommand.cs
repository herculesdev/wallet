using Flunt.Validations;
using MediatR;
using Wallet.Shared.Commands;
using Wallet.Shared.Results;

namespace Wallet.Domain.UseCases.Commands.Requests;

public class ApproveUserCommand : Command, IRequest<Result>
{
    public Guid UserId { get; init; }
    
    public ApproveUserCommand(Guid userId)
    {
        UserId = userId;
    }

    protected override void Validate()
    {
        AddNotifications(new Contract<bool>()
            .IsFalse(UserId == Guid.Empty, nameof(UserId), "Usuário informado é inválido"));
    }
}
