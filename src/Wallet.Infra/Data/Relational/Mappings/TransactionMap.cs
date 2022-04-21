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

        b.HasOne(t => t.From)
            .WithMany()
            .HasForeignKey(t => t.FromId);
        
        b.HasOne(t => t.To)
            .WithMany()
            .HasForeignKey(t => t.ToId);
        
        b.Property(t => t.FromId).HasColumnName("FromId");
        b.Property(t => t.ToId).HasColumnName("ToId");
        b.Property(a => a.Type).HasColumnName("Type");
        b.Property(t => t.ReferringId).HasColumnName("ReferringId");
        
        b.HasOne(t => t.Referring)
            .WithMany()
            .HasForeignKey(t => t.ReferringId);
        
        b.Property(t => t.Amount).HasColumnName("Amount");
        
        MapUtil.MapBaseFields(b);
    }
}