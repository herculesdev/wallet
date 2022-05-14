using Wallet.Domain.Enumerations;
using Wallet.Shared.Entities;

namespace Wallet.Domain.Entities;

public class Transaction : Entity
{
    private Account? _sourceAccount;
    public Account? SourceAccount
    {
        get => _sourceAccount;
        set
        {
            _sourceAccount = value;
            SourceAccountId = value?.Id;
        }
    }

    private Account _destinationAccount = new();
    public Account DestinationAccount
    {
        get => _destinationAccount;
        set
        {
            _destinationAccount = value;
            DestinationAccountId = value.Id;
        }
    }
    public Guid? SourceAccountId { get; set; }
    public Guid DestinationAccountId { get; set; } = Guid.Empty;
    public TransactionType Type { get; set; }
    public Transaction? Referring { get; set; }
    public Guid? ReferringId { get; set; }
    public decimal Amount { get; set; }

    private Transaction(Account? sourceAccount, Account destinationAccount, TransactionType type, Transaction? referring, decimal amount)
    {
        SourceAccount = sourceAccount;
        DestinationAccount = destinationAccount;
        Type = type;
        Referring = referring;
        Amount = amount;
    }
    
    public Transaction(
        Account sourceAccount, 
        Account destinationAccount, 
        TransactionType type, 
        decimal amount) : this(sourceAccount, destinationAccount, type, null, amount) { }
    
    public Transaction(
        Account destinationAccount, 
        TransactionType type, 
        decimal amount) : this(null, destinationAccount, type, null, amount) { }

    public Transaction()
    {
        
    }
}
