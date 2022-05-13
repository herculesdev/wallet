using Flunt.Notifications;

namespace Wallet.Shared.Query;

public abstract class MyNotifiable : Notifiable<Notification>
{
    public bool IsInvalid => !IsValid;

    public new bool IsValid
    {
        get
        {
            Clear();
            Validate();
            return base.IsValid;
        }
    }

    protected abstract void Validate();
}
