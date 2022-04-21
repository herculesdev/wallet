using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities.Base;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Common.Queries;
using Wallet.Infra.Data.Relational.Contexts;

namespace Wallet.Infra.Data.Relational.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly Context Db;

    protected Repository(Context db)
    {
        Db = db;
    }
    
    public virtual Task<T> AddAsync(T entity)
    {
        Db.Add(entity);
        return Task.FromResult(entity);
    }

    public virtual async Task<T?> GetAsync(Guid id)
    {
        var entity = await Db.Set<T>().FindAsync(id);
        return entity!;
    }

    public virtual Task<T> UpdateAsync(T entity)
    {
        Db.Set<T>().Update(entity);
        return Task.FromResult(entity);
    }

    public virtual Task DeleteAsync(T entity)
    {
        Db.Remove(entity);
        return Task.FromResult(0);
    }

    public virtual async Task Delete(Guid id)
    {
        var entity = await Db.Set<T>().FindAsync(id);
        
        if(entity != null)
            Db.Remove(entity);
    }

    protected async Task<PagedResult<TEntity>> Paginate<TEntity>(BasePagedQuery criteria, IQueryable<TEntity> query)
    {
        return new PagedResult<TEntity>(
            criteria.Page, 
            criteria.PageSize, 
            await query.CountAsync(),
            await query.ToListAsync());
    }
}