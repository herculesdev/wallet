using Wallet.Domain.Interfaces.Repositories.Relational;

namespace Wallet.Infra.Data.Relational.Contexts;

public class UnitOfWork : IUnitOfWork
{
    private Context _db;

    public UnitOfWork(Context db)
    {
        _db = db;
    }
    
    public async Task ReloadAsync<T>(T entity)
    {
        if(entity is null)
            return;
        
        await _db.Entry(entity).ReloadAsync();
    }

    public async Task BeginTransactionAsync()
        => await _db.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync()
        => await _db.Database.CommitTransactionAsync();
    
    public async Task RollbackTransactionAsync()
        => await _db.Database.RollbackTransactionAsync();
}
