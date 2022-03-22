using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entities;
using Wallet.Domain.Entities.User;

namespace Wallet.Infra.Data.Relational.Mappings;

public class BalanceMap : IEntityTypeConfiguration<Balance>
{
    public void Configure(EntityTypeBuilder<Balance> builder)
    {
        builder.ToTable("Balance")
            .HasKey(b => b.Id);

        builder.HasOne(b => b.Account)
            .WithMany()
            .HasForeignKey(b => b.AccountId);

        builder.Property(b => b.Transaction).HasColumnName("Transaction");
        builder.Property(b => b.IsDebit).HasColumnName("IsDebit");
        builder.Property(b => b.Value).HasColumnName("Value");
        builder.Property(b => b.FinalBalance).HasColumnName("FinalBalance");
        
        MapUtil.MapBaseFields(builder);

        builder.Ignore(b => b.IsCredit);
        builder.Ignore(b => b.Description);
    }
}
