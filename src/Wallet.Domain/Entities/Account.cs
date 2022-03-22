using Wallet.Domain.Entities.Base;
using Wallet.Domain.Enumerations;

namespace Wallet.Domain.Entities;

public class Account : BaseEntity
{
    public User.User Owner { get; set; } = new User.User();
    public Guid OwnerId { get; set; }
    public LegalNature Type { get; set; }
    public decimal Balance { get; set; }
}
