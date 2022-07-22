using MediatR;

namespace Wallet.Domain.Events.Handlers;

public class UserEventHandler : INotificationHandler<CreatedUserEvent>
{
    public UserEventHandler()
    {
        
    }
    
    public Task Handle(CreatedUserEvent ev, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
