using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entities;

namespace Wallet.Infra.Data.Relational.Mappings;

public class AccountMap : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> b)
    {
        b.ToTable("Account")
            .HasKey(a => a.Id);

        b.HasOne(a => a.Owner)
            .WithMany(o => o.Accounts)
            .HasForeignKey(a => a.OwnerId);

        b.Property(a => a.OwnerId).HasColumnName("OwnerId");
        b.Property(a => a.Type).HasColumnName("Type");
        b.Property(a => a.Balance).HasColumnName("Balance");
        b.Property(a => a.UpdatedAt).IsConcurrencyToken();
        
        MapUtil.MapBaseFields(b);
    }
}