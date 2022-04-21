namespace Wallet.Domain.Interfaces.Repositories.Relational;

public interface IRepository<TEntity>
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity?> GetAsync(Guid id);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task Delete(Guid id);
}