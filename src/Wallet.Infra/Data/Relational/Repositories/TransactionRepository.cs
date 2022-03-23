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
    
    public override async Task<Transaction> GetById(Guid id)
    {
        var entity = await Db.Set<Transaction>()
            .Include(t => t.From)
            .Include(t => t.To)
            .Include(t => t.Referring)
            .FirstOrDefaultAsync(t => t.Id == id);
        return entity!;
    }
    
    public async Task<bool> HasTransactionWith(Guid transactionId)
        => await Db.Transaction
            .AsNoTracking()
            .AnyAsync(t => t.Id == transactionId);
}
