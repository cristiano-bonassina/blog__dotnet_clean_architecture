using blog__dotnet_clean_architecture.Application;
using Lamar;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace blog__dotnet_clean_architecture.Infrastructure.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureContainerDefaults(this IHostBuilder hostBuilder) =>
        hostBuilder.UseLamar()
            .ConfigureContainer<ServiceRegistry>((_, services) =>
            {
                services.Scan(scanner =>
                {
                    scanner.TheCallingAssembly();
                    scanner.AssemblyContainingType<ApplicationAnchor>();
                    scanner.AssemblyContainingType<InfrastructureAnchor>();
                    scanner.WithDefaultConventions();
                });
            });
}
