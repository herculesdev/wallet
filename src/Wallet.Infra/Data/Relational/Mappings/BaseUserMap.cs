using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entities.User;

namespace Wallet.Infra.Data.Relational.Mappings;

public class BaseUserMap : IEntityTypeConfiguration<BaseUser>
{
    public void Configure(EntityTypeBuilder<BaseUser> b)
    {
        b.ToTable("User")
            .HasKey(u => u.Id);

        b.HasDiscriminator<string>("UserType");
        

        b.Property(u => u.Id)
            .HasColumnName("Id")
            .HasDefaultValueSql("uuid_generate_v4()");

        b.Property(u => u.Name)
            .HasMaxLength(64)
            .HasColumnName("Name");
        
        b.Property(u => u.LastName)
            .HasMaxLength(64)
            .HasColumnName("LastName");

        b.Property(u => u.BirthDate)
            .HasColumnType("Date")
            .HasColumnName("BirthDate");
        
        b.Property(u => u.Email)
            .HasMaxLength(64)
            .HasColumnName("Email");
        
        b.OwnsOne(u => u.Password, nav =>
        {
            nav.Property(pw => pw.EncryptedValue)
                .HasColumnName("EncryptedPassword");
            
            nav.Ignore(pw => pw.Value);
        });
        
        MapUtil.MapBaseFields(b);
        
        b.Ignore(u => u.FullName);
    }
}
