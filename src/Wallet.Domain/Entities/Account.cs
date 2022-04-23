using Wallet.Domain.Entities.Base;
using Wallet.Domain.Enumerations;

namespace Wallet.Domain.Entities;

public class Account : BaseEntity
{
    public User.User Owner { get; set; } = new();
    public Guid OwnerId { get; set; }
    public LegalNature Type { get; set; }

    private decimal _balance;
    public decimal Balance { 
        get => _balance;
        set
        {
            _balance = value;
            UpdatedBalanceAt = DateTime.UtcNow;
        }
    }
    public DateTime UpdatedBalanceAt { get; set; }

    public void UpdateBalanceValue(Balance balanceEvent)
    {
        if (balanceEvent.IsDebit)
            Balance -= balanceEvent.Value;
        else
            Balance += balanceEvent.Value;
    }
}
