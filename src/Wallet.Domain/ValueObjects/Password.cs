using Wallet.Shared.Helpers;
using Wallet.Shared.ValueObjects;

namespace Wallet.Domain.ValueObjects;

public class Password : BaseValueObject
{
    public string EncryptedValue { get; private set; } = string.Empty;
    public string Value { get => Crypto.Decrypt(EncryptedValue); init => EncryptedValue = Crypto.Encrypt(value); }
    
    public Password() { }

    public Password(string value)
    {
        Value = value;
    }

    public override string ToString() => EncryptedValue;

    public static implicit operator Password(string value) => new Password(value);
    protected override void Validate()
    {
        
    }
}
