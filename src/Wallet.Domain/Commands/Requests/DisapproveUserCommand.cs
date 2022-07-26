﻿using Flunt.Validations;
using MediatR;
using Wallet.Shared.Commands;
using Wallet.Shared.Results;

namespace Wallet.Domain.Commands.Requests;

public class DisapproveUserCommand : Command, IRequest<Result>
{
    public Guid UserId { get; init; }
    
    public DisapproveUserCommand(Guid userIdToApprove)
    {
        UserId = userIdToApprove;
    }

    protected override void Validate()
    {
        AddNotifications(new Contract<bool>()
            .IsFalse(UserId == Guid.Empty, nameof(UserId), "Usuário informado é inválido"));
    }
}
