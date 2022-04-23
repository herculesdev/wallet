namespace Wallet.Domain.Interfaces.Repositories.Relational;

public interface IUnitOfWork
{
    Task ReloadAsync<T>(T entity);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
