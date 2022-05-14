using Wallet.Domain.Entities.User;
using Wallet.Domain.Queries.Requests;
using Wallet.Shared.Entities;
using Wallet.Shared.Others;

namespace Wallet.Domain.Interfaces.Repositories.NonRelational;

public interface IUserReadRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<PagedResult<User>> GetByQueryAsync(GetAllUserQuery criteria);
}
