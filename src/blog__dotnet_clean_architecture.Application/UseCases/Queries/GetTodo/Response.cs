using System;
using blog__dotnet_clean_architecture.Domain.Entities;

namespace blog__dotnet_clean_architecture.Application.UseCases.Queries.GetTodo;

public record Response(
    DateTimeOffset CreatedAt,
    string Description,
    Id Id,
    bool IsCompleted,
    DateTimeOffset? UpdatedAt);
