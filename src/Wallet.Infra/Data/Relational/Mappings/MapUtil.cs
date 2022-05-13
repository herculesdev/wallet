using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wallet.Shared.Entities;

namespace Wallet.Infra.Data.Relational.Mappings;

public static class MapUtil
{
    public static void MapBaseFields<TEntity>(EntityTypeBuilder<TEntity> b) where TEntity : Entity
    {
        b.Property<bool>("IsDeleted").HasColumnName("IsDeleted");
        b.Property<DateTime>("DeletedAt").HasColumnName("DeletedAt");
        b.Property<DateTime>("CreatedAt").HasColumnName("CreatedAt");
        b.Property<DateTime>("UpdatedAt").HasColumnName("UpdatedAt");
        b.HasQueryFilter(e => !e.IsDeleted);
    }
}
