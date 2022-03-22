using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Domain.Entities.Base;
using Wallet.Domain.Entities.User;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Domain.UseCases.Queries.Requests;
using Wallet.Domain.ValueObjects;
using Wallet.Infra.Data.Relational.Contexts;

namespace Wallet.Infra.Data.Relational.Repositories;

public class BalanceRepository : Repository<Balance>, IBalanceRepository
{
    public BalanceRepository(Context db) : base(db)
    {
    }
}
