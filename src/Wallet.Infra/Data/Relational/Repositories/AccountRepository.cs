using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Infra.Data.Relational.Contexts;

namespace Wallet.Infra.Data.Relational.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(Context db) : base(db)
    {
    }

    async Task<Account> GetById(Guid id)
    {
        var entity = await Db.Set<Account>()
            .Include(a => a.Owner)
            .FirstOrDefaultAsync(a => a.Id == id);
        
        return entity!;
    }
    
    public async Task<bool> HasAccountWith(Guid accountId)
        => await Db.Accounts
            .AsNoTracking()
            .AnyAsync(a => a.Id == accountId);
}
