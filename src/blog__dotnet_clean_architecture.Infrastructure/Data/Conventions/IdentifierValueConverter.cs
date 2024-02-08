using System;
using blog__dotnet_clean_architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace blog__dotnet_clean_architecture.Infrastructure.Data.Conventions;

public class IdentifierValueConverter() : ValueConverter<Id, Guid>(x => x.Value, x => new Id(x));
