using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Wallet.Domain.Entities.User;
using Wallet.Domain.Interfaces.Repositories.NonRelational;
using Wallet.Domain.Queries.Requests;
using Wallet.Shared.Entities;
using Wallet.Shared.Others;

namespace Wallet.Infra.Data.NonRelational.Repositories;

public class UserReadRepository : ReadRepository, IUserReadRepository
{
    public UserReadRepository(IConfiguration config) : base(config)
    {
        
    }
    
    public async Task<User?> GetByIdAsync(Guid id)
    {
        var result = Users.Find(u => u.Id == id);
        return await result.FirstOrDefaultAsync();
    }

    public async Task<PagedResult<User>> GetByQueryAsync(GetAllUserQuery criteria)
    {
        var query = Users.Find(u =>
                u.Name.Contains(criteria.SearchString) ||
                u.LastName.Contains(criteria.SearchString) ||
                u.Document.ToString().Contains(criteria.SearchString) ||
                u.Email.Contains(criteria.SearchString))
            .Skip(criteria.Skip)
            .Limit(criteria.PageSize);

        var count = await query.CountDocumentsAsync();
        var data = await query.ToListAsync();

        return new PagedResult<User>(criteria.Page, criteria.PageSize, count, data);
    }
}
