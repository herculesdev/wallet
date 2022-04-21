using Flunt.Notifications;

namespace Wallet.Domain.ValueObjects.Base;

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