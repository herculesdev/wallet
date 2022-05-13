using Flunt.Notifications;

namespace Wallet.Shared.Commands;

public abstract class Command : Notifiable<Notification>
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
