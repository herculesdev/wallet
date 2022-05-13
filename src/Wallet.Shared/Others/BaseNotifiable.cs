using Flunt.Notifications;

namespace Wallet.Shared.Others;

public abstract class BaseNotifiable : Notifiable<Notification>
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
