using blog__dotnet_clean_architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace blog__dotnet_clean_architecture.Infrastructure.Data.Conventions;
public class DescriptionValueConverter : ValueConverter<Description, string>
{
    public DescriptionValueConverter() : base(x => x.ToString(), x => x)
    {
    }
}
