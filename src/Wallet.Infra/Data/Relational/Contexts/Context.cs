using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Entities;
using Wallet.Domain.Entities.Base;
using Wallet.Domain.Entities.User;

namespace Wallet.Infra.Data.Relational.Contexts;

public sealed class Context : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transaction => Set<Transaction>();
    public DbSet<Balance> Balance => Set<Balance>();
    
    public Context(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
    
    public override int SaveChanges()
    {
        AddTimestamps();
        ApplySoftDelete();
        return base.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        AddTimestamps();
        ApplySoftDelete();
        return await base.SaveChangesAsync();
    }
    
    private void AddTimestamps()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(x => x.Entity is BaseEntity)
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;
            var now = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
                entity.CreatedAt = now;

            entity.UpdatedAt = now;
        }
    }

    private void ApplySoftDelete()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(x => x.Entity is BaseEntity)
            .Where(x => x.State == EntityState.Deleted);

        foreach(var entry in entries)
        {
            var entity = (BaseEntity)entry.Entity;
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            entry.State = EntityState.Modified;
        }
    }
}
