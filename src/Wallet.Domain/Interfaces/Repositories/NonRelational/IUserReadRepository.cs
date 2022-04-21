using Wallet.Domain.Entities.Base;
using Wallet.Domain.Entities.User;
using Wallet.Domain.UseCases.Queries.Requests;

namespace Wallet.Domain.Interfaces.Repositories.NonRelational;

public interface IUserReadRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<PagedResult<User>> GetByQueryAsync(GetAllUserQuery criteria);
}