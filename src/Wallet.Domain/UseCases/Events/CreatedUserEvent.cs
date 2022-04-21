using MediatR;
using Wallet.Domain.Entities.User;
using Wallet.Domain.Events;

namespace Wallet.Domain.UseCases.Events;

public class CreatedUserEvent : BaseEvent<User>, INotification
{
    public CreatedUserEvent(User data) : base(data)
    {
        
    }
}