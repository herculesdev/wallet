namespace Wallet.Domain.UseCases.Queries.Responses;

public class BalanceResponse
{
    public Guid Id { get; init; }
    public bool IsDebit { get; init; }
    public decimal Value { get; init; }
    public string Description { get; init; }
    public DateTime CreatedAt { get; init; }

    public BalanceResponse(Guid id, bool isDebit, decimal value, string description, DateTime createdAt)
    {
        Id = id;
        IsDebit = isDebit;
        Value = value;
        Description = description;
        CreatedAt = createdAt;
    }
}
