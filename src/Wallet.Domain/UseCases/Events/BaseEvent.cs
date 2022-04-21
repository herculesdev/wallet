namespace Wallet.Domain.Events;

public abstract class BaseEvent<T>
{
    public T Data { get; init; }
    public DateTime OccurredIn { get; private set; }
    
    protected BaseEvent(T data)
    {
        Data = data;
        OccurredIn = DateTime.UtcNow;
    }
}