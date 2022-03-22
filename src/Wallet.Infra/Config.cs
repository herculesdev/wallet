using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Infra.Data.Relational.Contexts;
using Wallet.Infra.Data.Relational.Repositories;

namespace Wallet.Infra;

public static class Config
{
    public static IServiceCollection ConfigureWallet(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(o => 
            o.UseNpgsql(configuration.GetConnectionString("postgres")!));

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();
        services.AddTransient<IBalanceRepository, BalanceRepository>();
        services.AddTransient<ITokenGenerator, TokenGenerator>();

        return services;
    }
}
