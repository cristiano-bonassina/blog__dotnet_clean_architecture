using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using blog__dotnet_clean_architecture.Application.Data.Repositories;
using blog__dotnet_clean_architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace blog__dotnet_clean_architecture.Infrastructure.Data.Repositories;

public class ReadOnlyRepository<TEntity>(AppDbContext dbContext) : IReadOnlyRepository<TEntity>
    where TEntity : Entity, IAggregateRoot
{
    private readonly DbSet<TEntity> _entities = dbContext.Set<TEntity>();

    public virtual IQueryable<TEntity> Query()
    {
        return _entities.AsNoTracking().AsQueryable();
    }

    public virtual Task<TEntity?> GetByIdAsync(Id id, CancellationToken cancellationToken)
    {
        return _entities.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}