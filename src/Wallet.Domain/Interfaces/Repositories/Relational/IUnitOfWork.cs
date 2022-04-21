namespace Wallet.Domain.Interfaces.Repositories.Relational;

public interface IUnitOfWork
{
    bool Commit();
    Task<bool> CommitAsync();
}