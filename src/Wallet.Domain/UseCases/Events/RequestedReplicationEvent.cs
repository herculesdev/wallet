using Wallet.Shared.Events;

namespace Wallet.Domain.UseCases.Events;

public class RequestedReplicationEvent<TEntity> : Event<TEntity>
{
    public RequestedReplicationEvent(TEntity data) : base(data)
    {
        
    }
}

public static class RequestedReplicationEvent
{
    public static RequestedReplicationEvent<T> Create<T>(T data)
    {
        return new RequestedReplicationEvent<T>(data);
    }
}
