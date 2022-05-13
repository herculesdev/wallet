namespace Wallet.Shared.Events;

public abstract class Event<T>
{
    public T Data { get; init; }
    public DateTime OccurredIn { get; private set; }
    
    protected Event(T data)
    {
        Data = data;
        OccurredIn = DateTime.UtcNow;
    }
}
