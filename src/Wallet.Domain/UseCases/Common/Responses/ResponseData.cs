using Flunt.Notifications;

namespace Wallet.Domain.UseCases.Common.Responses;

public class ResponseData<T> : Response
{
    public T? Data { get; set; }
    
    public ResponseData<T> With(T data)
    {
        Data = data;
        return this;
    }
    
    public override ResponseData<T> AddNotifications(Notifiable<Notification> notifiable)
    {
        base.AddNotifications(notifiable);
        return this;
    }
    
    public override ResponseData<T> AddNotification(string key, string message)
    {
        base.AddNotification(key, message);
        return this;
    }
    
    public override ResponseData<T> AddNotification(string message)
    {
        base.AddNotification("", message);
        return this;
    }
}