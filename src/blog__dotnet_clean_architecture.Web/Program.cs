using System.Threading.Tasks;
using blog__dotnet_clean_architecture.Infrastructure.Extensions;
using LogicArt.Common.AzureFunctions;
using Microsoft.Extensions.Hosting;

namespace blog__dotnet_clean_architecture.Api.Web;

public static class Program
{
    public static async Task Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults(builder => builder.UseMiddleware<CultureMiddleware>())
            .ConfigureContainerDefaults()
            .ConfigureServices(services => services.ConfigureApiServices())
            .Build();
        await host.RunAsync();
    }
}