using Wallet.Domain.Entities.Base;
using Wallet.Domain.Entities.User;
using Wallet.Domain.UseCases.Queries.Requests;
using Wallet.Domain.ValueObjects;

namespace Wallet.Domain.Interfaces.Repositories.Relational;

public interface IUserRepository : IRepository<User>
{
    Task<bool> HasUserWith(Guid id);
    Task<bool> HasUserWith(DocumentNumber document, Password password);
    Task<bool> HasUserWith(DocumentNumber document);
    Task<bool> HasUserWithEmail(string email);
    Task<User> GetByAsync(DocumentNumber document, Password password);
    Task<PagedResult<User>> GetByAsync(GetAllUserQuery criteria);
}
