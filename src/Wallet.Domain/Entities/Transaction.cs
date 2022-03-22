using Wallet.Domain.Entities.Base;
using Wallet.Domain.Enumerations;

namespace Wallet.Domain.Entities;

public class Transaction : BaseEntity
{
    private Account? _from;
    public Account? From
    {
        get => _from;
        set
        {
            _from = value;
            FromId = value.Id;
        }
    }
    public Guid? FromId { get; set; }

    private Account _to = new();
    public Account To
    {
        get => _to ??= new();
        set
        {
            _to = value;
            ToId = value.Id;
        }
    }
    public Guid ToId { get; set; } = Guid.Empty;
    public TransactionType Type { get; set; }
    public Transaction? Referring { get; set; }
    public Guid? ReferringId { get; set; }
    public decimal Amount { get; set; }
}
