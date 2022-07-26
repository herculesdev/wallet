﻿using Flunt.Validations;
using MediatR;
using Wallet.Shared.Commands;
using Wallet.Shared.Results;

namespace Wallet.Domain.Commands.Requests;

public class CreateTransferCommand : Command, IRequest<Result>
{
    public Guid SourceAccountId { get; init; }
    public Guid DestinationAccountId { get; init; }
    public decimal Amount { get; init; }

    public CreateTransferCommand()
    {
        
    }
    
    public CreateTransferCommand(Guid sourceAccountId, Guid destinationAccountId, decimal amount)
    {
        SourceAccountId = sourceAccountId;
        DestinationAccountId = destinationAccountId;
        Amount = amount;
    }
    
    protected override void Validate()
    {
        AddNotifications(new Contract<bool>()
            .IsTrue(SourceAccountId != Guid.Empty, nameof(DestinationAccountId), "Conta de origem é obrigatória")
            .IsTrue(DestinationAccountId != Guid.Empty, nameof(DestinationAccountId), "Conta de destino é obrigatória")
            .IsGreaterThan(Amount, 0, nameof(Amount), "Valor é obrigatório"));
    }
}
