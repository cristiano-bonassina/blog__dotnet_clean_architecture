using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using blog__dotnet_clean_architecture.Application.Data.Repositories;
using blog__dotnet_clean_architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace blog__dotnet_clean_architecture.Infrastructure.Data.Repositories;

public class Repository<TEntity>(AppDbContext dbContext) : ReadOnlyRepository<TEntity>(dbContext), IRepository<TEntity>
    where TEntity : Entity, IAggregateRoot
{
    private readonly DbSet<TEntity> _entities = dbContext.Set<TEntity>();

    public void Add(TEntity entity)
    {
        _entities.Add(entity);
    }

    public void Remove(TEntity entity)
    {
        _entities.Remove(entity);
    }

    public override IQueryable<TEntity> Query()
    {
        return _entities.AsQueryable();
    }

    public override Task<TEntity?> GetByIdAsync(Id id, CancellationToken cancellationToken)
    {
        return _entities.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
