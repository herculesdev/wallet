namespace Wallet.Domain.Enumerations;

public enum TransactionType
{
    Withdraw = 1,
    Deposit,
    Reversal,
    Transfer,
    Payment,
    CardVerification
}