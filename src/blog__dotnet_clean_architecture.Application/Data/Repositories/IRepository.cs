using blog__dotnet_clean_architecture.Domain.Entities;

namespace blog__dotnet_clean_architecture.Application.Data.Repositories;

public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : Entity, IAggregateRoot
{
    void Add(TEntity entity);

    void Remove(TEntity entity);
}
