using System;

namespace blog__dotnet_clean_architecture.Domain.Entities;

public readonly record struct Id(Guid Value)
{
    public static implicit operator Guid(Id id) => id.Value;

    public static implicit operator Id(Guid value) => new(value);
}
