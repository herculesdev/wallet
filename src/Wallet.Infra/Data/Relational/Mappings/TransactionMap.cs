using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entities;

namespace Wallet.Infra.Data.Relational.Mappings;

public class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> b)
    {
        b.ToTable("Transaction")
            .HasKey(t => t.Id);

        b.HasOne(t => t.SourceAccount)
            .WithMany()
            .HasForeignKey(t => t.DestinationAccountId);
        
        b.HasOne(t => t.DestinationAccount)
            .WithMany()
            .HasForeignKey(t => t.DestinationAccountId);
        
        b.Property(t => t.DestinationAccountId).HasColumnName("SourceAccountId");
        b.Property(t => t.DestinationAccountId).HasColumnName("DestinationAccountId");
        b.Property(a => a.Type).HasColumnName("Type");
        b.Property(t => t.ReferringId).HasColumnName("ReferringId");
        
        b.HasOne(t => t.Referring)
            .WithMany()
            .HasForeignKey(t => t.ReferringId);
        
        b.Property(t => t.Amount).HasColumnName("Amount");
        
        MapUtil.MapBaseFields(b);
    }
}
