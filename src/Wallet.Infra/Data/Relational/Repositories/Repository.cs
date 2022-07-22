using System.Data;
using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Infra.Data.Relational.Contexts;
using Wallet.Shared.Others;
using Wallet.Shared.Queries;

namespace Wallet.Infra.Data.Relational.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly Context Db;

    protected Repository(Context db)
    {
        Db = db;
    }
    
    public virtual async Task<T> AddAsync(T entity)
    {
        Db.Add(entity);
        await Db.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T?> GetAsync(Guid id)
    {
        var entity = await Db.Set<T>().FindAsync(id);
        return entity!;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        try
        {
            Db.Set<T>().Update(entity);
            await Db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new DBConcurrencyException(e.Message, e);
        }
        
        return entity;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        Db.Remove(entity);
        await Db.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await Db.Set<T>().FindAsync(id);
        
        if(entity != null)
            Db.Remove(entity);

        await Db.SaveChangesAsync();
    }

    protected async Task<PagedResult<TEntity>> PaginateAsync<TEntity>(PagedQuery criteria, IQueryable<TEntity> query)
    {
        return new PagedResult<TEntity>(
            criteria.Page, 
            criteria.PageSize, 
            await query.CountAsync(),
            await query.ToListAsync());
    }
}
