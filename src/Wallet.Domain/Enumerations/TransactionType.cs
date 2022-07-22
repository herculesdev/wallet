namespace Wallet.Domain.Enumerations;

public enum TransactionType
{
    Withdraw = 1,
    Deposit = 2,
    Reversal = 3,
    Transfer = 4,
    Payment = 5,
    CardVerification = 6
}

public static class TransactionTypeExtensions
{
    public static bool IsWithdraw(this TransactionType type) => type == TransactionType.Withdraw;
    public static bool IsDeposit(this TransactionType type) => type == TransactionType.Deposit;
    public static bool IsReversal(this TransactionType type) => type == TransactionType.Reversal;
    public static bool IsTransfer(this TransactionType type) => type == TransactionType.Transfer;
    public static bool IsPayment(this TransactionType type) => type == TransactionType.Payment;
    public static bool IsCardVerification(this TransactionType type) => type == TransactionType.CardVerification;
}
