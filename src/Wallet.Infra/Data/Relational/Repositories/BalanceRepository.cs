using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Infra.Data.Relational.Contexts;

namespace Wallet.Infra.Data.Relational.Repositories;

public class BalanceRepository : Repository<Balance>, IBalanceRepository
{
    public BalanceRepository(Context db) : base(db)
    {
    }

    public override async Task<Balance?> GetAsync(Guid id)
    {
        var balance = await Db.Set<Balance>()
            .Include(b => b.Transaction)
            .FirstOrDefaultAsync(b => b.Id == id);

        return balance!;
    }
}