using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Domain.Entities.User;

namespace Wallet.Infra.Data.Relational.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> b)
    {
        b.OwnsOne(u => u.Document, nav =>
        {
            nav.Property(dn => dn.Number)
                .HasColumnName("DocumentNumber")
                .HasMaxLength(14);

            nav.Ignore(dn => dn.Notifications);
            nav.Ignore(dn => dn.IsValid);
            nav.Ignore(dn => dn.IsCpf);
            nav.Ignore(dn => dn.IsCnpj);
        });

        b.Property(u => u.Nature)
            .HasColumnName("Nature");

        b.Property(u => u.RegistrationStatus)
            .HasColumnName("RegistrationStatus");
        
        b.Property(u => u.RegistrationStatusUpdatedAt)
            .HasColumnName("RegistrationStatusUpdatedAt");
    }
}