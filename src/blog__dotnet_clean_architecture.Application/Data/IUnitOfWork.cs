using System.Threading;
using System.Threading.Tasks;

namespace blog__dotnet_clean_architecture.Application.Data;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken);
}