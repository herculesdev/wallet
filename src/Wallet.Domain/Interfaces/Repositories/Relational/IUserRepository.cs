using Wallet.Domain.Entities.User;
using Wallet.Domain.Queries.Requests;
using Wallet.Domain.ValueObjects;
using Wallet.Shared.Entities;
using Wallet.Shared.Others;

namespace Wallet.Domain.Interfaces.Repositories.Relational;

public interface IUserRepository : IRepository<User>
{
    Task<bool> HasUserWithDocumentAsync(DocumentNumber document);
    Task<bool> HasUserWithEmailAsync(string email);
    Task<User?> GetAsync(DocumentNumber document, Password password);
    Task<PagedResult<User>> GetAsync(GetAllUserQuery criteria);
}
