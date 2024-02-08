using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace blog__dotnet_clean_architecture.Infrastructure.Data.Schema;

public class DatabaseMigrationService(AppDbContext dbContext) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return dbContext.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

