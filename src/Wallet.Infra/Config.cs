using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Interfaces.Integrations.Messaging;
using Wallet.Domain.Interfaces.Repositories.NonRelational;
using Wallet.Domain.Interfaces.Repositories.Relational;
using Wallet.Infra.Data.NonRelational.Repositories;
using Wallet.Infra.Data.Relational.Contexts;
using Wallet.Infra.Data.Relational.Repositories;
using Wallet.Infra.Integration.RabbitMq;

namespace Wallet.Infra;

public static class Config
{
    public static IServiceCollection ConfigureWallet(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context>(o => 
            o.UseNpgsql(configuration.GetConnectionString("postgres")!));
        
        services.AddSingleton(configuration);
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserReadRepository, UserReadRepository>();
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<ITransactionRepository, TransactionRepository>();
        services.AddTransient<IBalanceRepository, BalanceRepository>();
        services.AddTransient<ISession, Session>();
        services.AddTransient<ITokenGenerator, TokenGenerator>();
        services.AddTransient<IQueueHandler, RabbitMq>();

        return services;
    }
}