using blog__dotnet_clean_architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using UUIDNext;

namespace blog__dotnet_clean_architecture.Infrastructure.Data.Conventions;

public class IdentifierValueGenerator : ValueGenerator<Id>
{
    public override Id Next(EntityEntry entry) => new(Uuid.NewSequential());

    public override bool GeneratesTemporaryValues => false;
}
