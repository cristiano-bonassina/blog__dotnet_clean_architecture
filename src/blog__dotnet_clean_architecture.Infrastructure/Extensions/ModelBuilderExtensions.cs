using System.Linq;
using blog__dotnet_clean_architecture.Infrastructure.Data.Conventions;
using Microsoft.EntityFrameworkCore;

namespace blog__dotnet_clean_architecture.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyNamingConventions(this ModelBuilder builder)
    {
        var entityTypes = builder.Model.GetEntityTypes()
            .Where(x => x.BaseType == null)
            .ToList();
        entityTypes.ForEach(entityType =>
        {
            entityType.SetTableName(SnakeCaseNameTranslator.TranslateTypeName(entityType));
            foreach (var property in entityType.GetProperties())
            {
                property.SetColumnName(SnakeCaseNameTranslator.TranslateMemberName(property));
            }
        });

        return builder;
    }
}
