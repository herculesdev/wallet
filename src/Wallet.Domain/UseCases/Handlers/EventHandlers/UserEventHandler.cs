using Wallet.Domain.Events;

namespace Wallet.Domain.UseCases.Handlers.EventHandlers;

public class UserEventHandler : MediatR.INotificationHandler<CreatedUserEvent>
{
    public async Task Handle(CreatedUserEvent ev, CancellationToken cancellationToken)
    {
        var user = ev.Data;
        await Task.Delay(1);
        // replica numa base de leitura
    }
}
