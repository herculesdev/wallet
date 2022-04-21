using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entities;

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

        builder.HasOne(b => b.Transaction)
            .WithMany()
            .HasForeignKey(b => b.TransactionId);
        
        builder.Property(b => b.TransactionId).HasColumnName("TransactionId");
        builder.Property(b => b.IsDebit).HasColumnName("IsDebit");
        builder.Property(b => b.Value).HasColumnName("Value");

        MapUtil.MapBaseFields(builder);

        builder.Ignore(b => b.IsCredit);
        builder.Ignore(b => b.Description);
    }
}