namespace Wallet.Domain.Events;

public abstract class BaseEvent<T>
{
    public T Data { get; init; }
    public DateTime Date { get; private set; } = DateTime.UtcNow;
    protected BaseEvent(T data)
    {
        Data = data;
    }
}
