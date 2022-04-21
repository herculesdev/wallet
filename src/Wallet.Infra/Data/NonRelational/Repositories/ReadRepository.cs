using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Wallet.Domain.Entities.User;

namespace Wallet.Infra.Data.NonRelational.Repositories;

public abstract class ReadRepository
{
    protected readonly IMongoCollection<User> Users;

    protected ReadRepository(IConfiguration config)
    {
        var connectionString = config.GetConnectionString("mongodb");
        var mongoUrl = new MongoUrl(connectionString);
        var mongoClient = new MongoClient(connectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        
        Users = mongoDatabase.GetCollection<User>("user");
    }
}