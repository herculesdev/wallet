using Wallet.Domain.Entities;

namespace Wallet.Domain.Interfaces.Repositories.Relational;

public interface IRepository<TEntity>
{
    Task<TEntity> Add(TEntity entity);
    Task<TEntity> GetById(Guid id);
    Task<TEntity> Update(TEntity entity);
    Task Delete(TEntity entity);
    Task Delete(Guid id);
}
