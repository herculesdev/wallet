using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Infra.Data.Relational.Contexts;

namespace Wallet.Infra.Data.Relational.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(Context db) : base(db)
    {
        
    }
}