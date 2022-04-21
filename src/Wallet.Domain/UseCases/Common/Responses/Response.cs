using Flunt.Notifications;
using Wallet.Domain.Helpers.Extensions;

namespace Wallet.Domain.UseCases.Common.Responses;

public class Response : Notifiable<Notification>
{
    public IReadOnlyCollection<Notification> FieldErrors
        => Notifications.Where(n => n.Key.IsNotEmpty())
            .ToList();
    
    public IReadOnlyCollection<string?> FlowErrors 
        => Notifications.Where(n => n.Key.IsEmpty())
            .Select(n => n.Message)
            .ToList();

    public string? FirstFlowError => FlowErrors.FirstOrDefault();
    
    
    
    public virtual new Response AddNotifications(Notifiable<Notification> notifiable)
    {
        base.AddNotifications(notifiable);
        return this;
    }
    
    public virtual new Response AddNotification(string key, string message)
    {
        base.AddNotification(key, message);
        return this;
    }
    
    public virtual Response AddNotification(string message)
    {
        base.AddNotification("", message);
        return this;
    }

}