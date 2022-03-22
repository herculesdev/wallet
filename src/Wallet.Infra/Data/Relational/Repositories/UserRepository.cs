using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities.Base;
using Wallet.Domain.Entities.User;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Queries.Requests;
using Wallet.Domain.ValueObjects;
using Wallet.Infra.Data.Relational.Contexts;

namespace Wallet.Infra.Data.Relational.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(Context db) : base(db)
    {
    }
    
    public async Task<bool> HasUserWith(Guid id)
        => await Db.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == id);
    public async Task<bool> HasUserWith(DocumentNumber document)
        => await Db.Users
            .AsNoTracking()
            .AnyAsync(u => u.Document.Number == document.Number);

    public async Task<bool> HasUserWith(DocumentNumber document, Password password)
        => await Db.Users
            .AsNoTracking()
            .Where(u => u.Document.Number == document.Number)
            .Where(u => u.Password.EncryptedValue == password.EncryptedValue)
            .AnyAsync();
    

    public async Task<bool> HasUserWithEmail(string email) 
        => await Db.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == email);

    public async Task<User> GetByAsync(DocumentNumber document, Password password)
    {
        var user = await Db.Users
            .Where(u => u.Document.Number == document.Number)
            .Where(u => u.Password.EncryptedValue == password.EncryptedValue)
            .FirstOrDefaultAsync();

        return user!;
    }

    public async Task<User> GetByAsync(Guid id)
    {
        var user = await Db.Users
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        return user!;
    }

    public async Task<PagedResult<User>> GetByAsync(GetAllUserQuery criteria)
    {
        var query = Db.Users
            .AsNoTracking()
            .Where(u => 
                EF.Functions.Like(string.Concat(u.Name, " ", u.LastName), $"%{criteria.SearchString}%") ||
                EF.Functions.Like(u.Document.Number, $"%{criteria.SearchString}") ||
                EF.Functions.Like(u.Email, $"%{criteria.SearchString}"))
            .Skip(criteria.Skip)
            .Take(criteria.PageSize);

        return await Paginate(criteria, query);
    }
}
