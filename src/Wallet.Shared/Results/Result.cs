using Flunt.Notifications;
using Wallet.Shared.Helpers.Extensions;

namespace Wallet.Shared.Results;

public class Result : Notifiable<Notification>
{
    public IReadOnlyCollection<Notification> FieldErrors
        => Notifications.Where(n => n.Key.IsNotEmpty())
            .ToList();
    
    public IEnumerable<string?> FlowErrors 
        => Notifications.Where(n => n.Key.IsEmpty())
            .Select(n => n.Message)
            .ToList();

    public string? FirstFlowError => FlowErrors.FirstOrDefault();
    
    
    
    public new virtual Result AddNotifications(Notifiable<Notification> notifiable)
    {
        base.AddNotifications(notifiable);
        return this;
    }
    
    public new virtual Result AddNotification(string key, string message)
    {
        base.AddNotification(key, message);
        return this;
    }
    
    public virtual Result AddNotification(string message)
    {
        base.AddNotification("", message);
        return this;
    }

}
