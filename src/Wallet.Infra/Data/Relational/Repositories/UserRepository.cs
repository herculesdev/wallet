using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities.User;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Queries.Requests;
using Wallet.Domain.ValueObjects;
using Wallet.Infra.Data.Relational.Contexts;
using Wallet.Shared.Entities;
using Wallet.Shared.Others;

namespace Wallet.Infra.Data.Relational.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(Context db) : base(db)
    {
    }
    
    public async Task<bool> HasUserWithAsync(Guid id)
        => await Db.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == id);
    public async Task<bool> HasUserWithAsync(DocumentNumber document)
        => await Db.Users
            .AsNoTracking()
            .AnyAsync(u => u.Document.Number == document.Number);

    public async Task<bool> HasUserWithAsync(DocumentNumber document, Password password)
        => await Db.Users
            .AsNoTracking()
            .Where(u => u.Document.Number == document.Number)
            .Where(u => u.Password.EncryptedValue == password.EncryptedValue)
            .AnyAsync();
    

    public async Task<bool> HasUserWithEmailAsync(string email) 
        => await Db.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == email);

    public async Task<User> GetAsync(DocumentNumber document, Password password)
    {
        var user = await Db.Users
            .Where(u => u.Document.Number == document.Number)
            .Where(u => u.Password.EncryptedValue == password.EncryptedValue)
            .FirstOrDefaultAsync();

        return user!;
    }

    public override async Task<User?> GetAsync(Guid id)
    {
        var user = await Db.Users
            .Include(u => u.Accounts)
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        return user!;
    }

    public async Task<PagedResult<User>> GetAsync(GetAllUserQuery criteria)
    {
        var query = Db.Users
            .AsNoTracking()
            .Where(u => 
                EF.Functions.Like(string.Concat(u.Name, " ", u.LastName), $"%{criteria.SearchString}%") ||
                EF.Functions.Like(u.Document.Number, $"%{criteria.SearchString}") ||
                EF.Functions.Like(u.Email, $"%{criteria.SearchString}"))
            .Skip(criteria.Skip)
            .Take(criteria.PageSize);

        return await PaginateAsync(criteria, query);
    }
}
