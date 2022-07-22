using Flunt.Notifications;

namespace Wallet.Shared.ValueObjects;

public abstract class BaseValueObject : Notifiable<Notification>
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
