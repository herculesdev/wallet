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
    
    public Task<T> Add(T entity)
    {
        Db.Add(entity);
        return Task.FromResult(entity);
    }

    public async Task<T> GetById(Guid id)
    {
        var entity = await Db.Set<T>().FindAsync(id);
        return entity!;
    }

    public Task<T> Update(T entity)
    {
        Db.Set<T>().Update(entity);
        return Task.FromResult(entity);
    }

    public Task Delete(T entity)
    {
        Db.Remove(entity);
        return Task.FromResult(0);
    }

    public async Task Delete(Guid id)
    {
        var entity = await Db.Set<T>().FindAsync(id);
        
        if(entity != null)
            Db.Remove(entity);
    }

    protected async Task<PagedResult<T>> Paginate<T>(BasePagedQuery criteria, IQueryable<T> query)
    {
        return new PagedResult<T>(
            criteria.Page, 
            criteria.PageSize, 
            await query.CountAsync(),
            await query.ToListAsync());
    }
}
