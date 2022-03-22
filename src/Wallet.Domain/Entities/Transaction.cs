using Wallet.Domain.Entities.Base;
using Wallet.Domain.Enumerations;

namespace Wallet.Domain.Entities;

public class Transaction : BaseEntity
{
    public Account? From { get; set; }
    public Guid? FromId { get; set; }
    public Account To { get; set; } = new();
    public Guid ToId { get; set; } = Guid.Empty;
    public TransactionType Type { get; set; }
    public Transaction? Referring { get; set; }
    public Guid? ReferringId { get; set; }
    public decimal Amount { get; set; }
}
