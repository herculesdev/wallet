using Wallet.Domain.Entities;

namespace Wallet.Domain.Interfaces.Repositories.Relational;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<bool> HasTransactionWith(Guid transactionId);
}
