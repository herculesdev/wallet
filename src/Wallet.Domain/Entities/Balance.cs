using Wallet.Domain.Enumerations;
using Wallet.Shared.Entities;
using Wallet.Shared.Helpers.Extensions;

namespace Wallet.Domain.Entities;

public class Balance : Entity
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

    public string Description
    {
        get
        {
            var type = Transaction.Type;
            var currencyValue = Value.ToCurrency();
            
            if (IsDebit && type == TransactionType.Withdraw)
                return $"Saque: {currencyValue}";

            if (IsCredit && type == TransactionType.Deposit)
                return $"Depósito: {currencyValue}";
            
            if (IsCredit && type == TransactionType.Transfer)
                return $"Transferência recebida: {currencyValue}";
            
            if (IsDebit && type == TransactionType.Transfer)
                return $"Transferência realizada: {currencyValue}";
            
            if (IsCredit && type == TransactionType.Payment)
                return $"Pagamento recebido: {currencyValue}";
            
            if (IsDebit && type == TransactionType.Transfer)
                return $"Pagamento realizado: {currencyValue}";
            
            if (IsCredit && type == TransactionType.CardVerification)
                return $"Cobrança de verificação de cartão: {currencyValue}";

            if (type == TransactionType.Reversal)
                return $"Estorno: ${currencyValue}";

            return "";
        }
    }
}
