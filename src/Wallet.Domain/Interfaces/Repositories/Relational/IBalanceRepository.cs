using Wallet.Domain.Entities;
using Wallet.Domain.UseCases.Queries.Requests;

namespace Wallet.Domain.Interfaces.Repositories.Relational;

public interface IBalanceRepository : IRepository<Balance>
{
    public Task<List<Balance>> GetAsync(GetBalanceHistoryQuery query);
}
