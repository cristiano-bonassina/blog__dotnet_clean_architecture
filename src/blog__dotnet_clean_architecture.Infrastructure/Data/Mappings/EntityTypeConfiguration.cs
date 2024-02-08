using blog__dotnet_clean_architecture.Domain.Entities;
using blog__dotnet_clean_architecture.Infrastructure.Data.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blog__dotnet_clean_architecture.Infrastructure.Data.Mappings;

public class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasValueGenerator<IdentifierValueGenerator>()
            .ValueGeneratedOnAdd();
    }
}
