using Wallet.Domain.Interfaces.Repositories.Relational;

namespace Wallet.Infra.Data.Relational.Contexts;

public class UnitOfWork : IUnitOfWork
{
    private Context _db;
    
    public UnitOfWork(Context db)
    {
        _db = db;
    }
    
    public bool Commit()
    {
        return _db.SaveChanges() > 1;
    }

    public async Task<bool> CommitAsync()
    {
        return await _db.SaveChangesAsync() > 1;
    }
}