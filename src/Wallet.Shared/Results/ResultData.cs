using Flunt.Notifications;

namespace Wallet.Shared.Results;

public class ResultData<T> : Result
{
    public T? Data { get; set; }
    
    public ResultData<T> WithData(T data)
    {
        Data = data;
        return this;
    }
    
    public override ResultData<T> AddNotifications(Notifiable<Notification> notifiable)
    {
        base.AddNotifications(notifiable);
        return this;
    }
    
    public override ResultData<T> AddNotification(string key, string message)
    {
        base.AddNotification(key, message);
        return this;
    }
    
    public override ResultData<T> AddNotification(string message)
    {
        base.AddNotification("", message);
        return this;
    }

    public ResultData()
    {
        
    }
    
    public ResultData(T data)
    {
        Data = data;
    }
}
