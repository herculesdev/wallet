using MediatR;
using Wallet.Domain.Enumerations;
using Wallet.Domain.Interfaces.Integrations.Messaging;
using Wallet.Domain.UseCases.Events;

namespace Wallet.Domain.UseCases.Handlers.EventHandlers;

public class UserEventHandler : INotificationHandler<CreatedUserEvent>
{
    private readonly IQueueHandler _queueHandler;
    public UserEventHandler(IQueueHandler queueHandler)
    {
        _queueHandler = queueHandler;
    }
    
    public Task Handle(CreatedUserEvent ev, CancellationToken cancellationToken)
    {
        _queueHandler.Publish(RequestedReplicationEvent.Create(ev.Data), QueueExchange.Replication);
        return Task.CompletedTask;
    }
}