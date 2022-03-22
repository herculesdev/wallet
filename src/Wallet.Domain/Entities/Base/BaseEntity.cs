namespace Wallet.Domain.Entities.Base;

public abstract class BaseEntity
{
    public Guid Id { get; init; }
    public bool IsDeleted { get; set; } = false;
    public DateTime DeletedAt { get; set; } =  DateTime.MinValue;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
