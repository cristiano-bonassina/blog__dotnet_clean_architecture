using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using blog__dotnet_clean_architecture.Domain.Entities;

namespace blog__dotnet_clean_architecture.Infrastructure.Data.Serialization;

public class IdJsonConverter : JsonConverter<Id>
{
    public override Id Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value == null ? new Id() : new Id(new Guid(value));
    }

    public override void Write(Utf8JsonWriter writer, Id value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
