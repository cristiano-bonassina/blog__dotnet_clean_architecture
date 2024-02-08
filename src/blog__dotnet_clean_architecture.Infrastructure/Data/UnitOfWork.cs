using System.Threading;
using System.Threading.Tasks;
using blog__dotnet_clean_architecture.Application.Data;

namespace blog__dotnet_clean_architecture.Infrastructure.Data;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
