using MediatR;
using Wallet.Domain.Entities.User;
using Wallet.Shared.Events;

namespace Wallet.Domain.UseCases.Events;

public class CreatedUserEvent : Event<User>, INotification
{
    public CreatedUserEvent(User? data) : base(data)
    {
        
    }
}
