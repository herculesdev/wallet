using Wallet.Domain.Entities.Base;
using Wallet.Domain.Enumerations;

namespace Wallet.Domain.Entities;

public class Balance : BaseEntity
{
    public Account Account { get; set; } = new();
    public Guid AccountId { get; set; }

    private Transaction _transaction = new();
    public Transaction Transaction
    {
        get => _transaction ??= new ();
        set
        {
            _transaction = value;
            TransactionId = value.Id;
        }
    }
    public Guid TransactionId { get; set; }
    public bool IsDebit { get; set; }
    public bool IsCredit => !IsDebit;
    public decimal Value { get; set; }
    private string Signal => IsDebit ? "-" : "";

    public string Description
    {
        get
        {
            var type = Transaction.Type;
            if (IsDebit && type == TransactionType.Withdraw)
                return $"Saque: {-Value}";

            if (IsCredit && type == TransactionType.Deposit)
                return $"Depósito: ${Value}";
            
            if (IsCredit && type == TransactionType.Transfer)
                return $"Transferência recebida: ${Value}";
            
            if (IsDebit && type == TransactionType.Transfer)
                return $"Transferência realizada: -${Value}";
            
            if (IsCredit && type == TransactionType.Payment)
                return $"Pagamento recebido: ${Value}";
            
            if (IsDebit && type == TransactionType.Transfer)
                return $"Pagamento realizado: -${Value}";
            
            if (IsCredit && type == TransactionType.CardVerification)
                return $"Cobrança de verificação de cartão: -${Value}";

            if (type == TransactionType.Reversal)
                return $"Estorno: ${Signal}${Value}";

            return "";
        }
    }
}