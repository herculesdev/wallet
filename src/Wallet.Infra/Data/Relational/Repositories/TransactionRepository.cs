using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Infra.Data.Relational.Contexts;

namespace Wallet.Infra.Data.Relational.Repositories;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(Context db) : base(db)
    {
    }
    
    public override async Task<Transaction?> GetAsync(Guid id)
    {
        var entity = await Db.Set<Transaction>()
            .Include(t => t.SourceAccount)
            .Include(t => t.DestinationAccount)
            .Include(t => t.Referring)
            .FirstOrDefaultAsync(t => t.Id == id);
        return entity!;
    }
}