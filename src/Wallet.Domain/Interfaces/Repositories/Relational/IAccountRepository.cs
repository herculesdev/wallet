using Wallet.Domain.Entities;

namespace Wallet.Domain.Interfaces.Repositories.Relational;

public interface IAccountRepository : IRepository<Account>
{
    Task<bool> HasAccountWith(Guid commandDestinationAccountId);
}
