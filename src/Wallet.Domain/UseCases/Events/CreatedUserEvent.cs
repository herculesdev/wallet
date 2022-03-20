using MediatR;
using Wallet.Domain.Entities;
using Wallet.Domain.Entities.User;

namespace Wallet.Domain.Events;

public class CreatedUserEvent : BaseEvent<User>, INotification
{
    public CreatedUserEvent(User data) : base(data)
    {
        
    }
}
