using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace blog__dotnet_clean_architecture.Infrastructure.Data.Conventions;

public static partial class SnakeCaseNameTranslator
{
    [GeneratedRegex("(?<=[a-z])([A-Z])")]
    private static partial Regex ConvertToSnakeCaseRegex();

    private static string ConvertToSnakeCaseName(string name) =>
        ConvertToSnakeCaseRegex().Replace(name, "_$1").ToLower();

    public static string TranslateTypeName(IMutableEntityType entityType) =>
        ConvertToSnakeCaseName(entityType.GetTableName()!);

    public static string TranslateMemberName(IMutableProperty property)
    {
        var columnName = property.IsPrimaryKey() ? $"{property.DeclaringType.ClrType.Name}Id" : property.Name;

        return ConvertToSnakeCaseName(columnName);
    }
}
