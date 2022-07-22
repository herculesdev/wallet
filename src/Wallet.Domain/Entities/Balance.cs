using Wallet.Domain.Enumerations;
using Wallet.Shared.Entities;
using Wallet.Shared.Helpers.Extensions;

namespace Wallet.Domain.Entities;

public class Balance : Entity
{
    private Account _account = new();

    public Account Account
    {
        get => _account;
        set
        {
            _account = value;
            AccountId = _account.Id;
        }
    }
    public Guid AccountId { get; set; }

    private Transaction _transaction = new();
    public Transaction Transaction
    {
        get => _transaction;
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
            
            if (IsDebit && type.IsWithdraw())
                return $"Saque: {currencyValue}";

            if (IsCredit && type.IsDeposit())
                return $"Depósito: {currencyValue}";
            
            if (IsCredit && type.IsTransfer())
                return $"Transferência recebida: {currencyValue}";
            
            if (IsDebit && type.IsTransfer())
                return $"Transferência realizada: {currencyValue}";
            
            if (IsCredit && type.IsPayment())
                return $"Pagamento recebido: {currencyValue}";
            
            if (IsDebit && type.IsPayment())
                return $"Pagamento realizado: {currencyValue}";
            
            if (IsCredit && type.IsCardVerification())
                return $"Cobrança de verificação de cartão: {currencyValue}";

            if (type.IsReversal())
                return $"Estorno: ${currencyValue}";

            return "";
        }
    }

    public Balance()
    {
        
    }
    
    public Balance(Account account, Transaction transaction, bool isDebit, decimal value)
    {
        Account = account;
        Transaction = transaction;
        IsDebit = isDebit;
        Value = value;
    }
}
