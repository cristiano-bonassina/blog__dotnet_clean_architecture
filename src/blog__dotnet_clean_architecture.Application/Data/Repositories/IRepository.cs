﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using blog__dotnet_clean_architecture.Domain.Entities;

namespace blog__dotnet_clean_architecture.Application.Data.Repositories;

public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
{
    IQueryable<TEntity> Query();

    Task<TEntity?> GetByIdAsync(Id id, CancellationToken cancellationToken);

    void Add(TEntity entity);

    void Remove(TEntity entity);
}
