using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using blog__dotnet_clean_architecture.Domain;
using blog__dotnet_clean_architecture.Domain.Entities;
using blog__dotnet_clean_architecture.Infrastructure.Data.Conventions;
using blog__dotnet_clean_architecture.Infrastructure.Data.Mappings;
using blog__dotnet_clean_architecture.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace blog__dotnet_clean_architecture.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Id>().HaveConversion<IdentifierValueConverter>();
        configurationBuilder.Properties<Description>().HaveConversion<DescriptionValueConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var applyEntityConfigurationMethod = typeof(ModelBuilder)
            .GetMethods()
            .Single(x => x is { Name: nameof(modelBuilder.ApplyConfiguration), ContainsGenericParameters: true }
                         && x.GetParameters().SingleOrDefault()?.ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
        var entityTypes = typeof(DomainAnchor).Assembly.GetTypes()
            .Where(type => type is { IsAbstract: false, IsGenericTypeDefinition: false })
            .Where(type => typeof(Entity).IsAssignableFrom(type));
        foreach (var entityType in entityTypes)
        {
            var entityTypeConfigurationType = typeof(EntityTypeConfiguration<>).MakeGenericType(entityType);
            var entityTypeConfiguration = Activator.CreateInstance(entityTypeConfigurationType);
            var target = applyEntityConfigurationMethod.MakeGenericMethod(entityType);
            target.Invoke(modelBuilder, [entityTypeConfiguration]);
        }

        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(InfrastructureAnchor).Assembly)
            .HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotifications)
            .ApplyNamingConventions();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(x => x is { Entity: Entity, State: EntityState.Added or EntityState.Modified });

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (((Entity)entry.Entity).CreatedAt == default)
                    {
                        ((Entity)entry.Entity).CreatedAt = DateTimeOffset.UtcNow;
                    }

                    break;
                case EntityState.Modified:
                    ((Entity)entry.Entity).UpdatedAt = DateTimeOffset.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
