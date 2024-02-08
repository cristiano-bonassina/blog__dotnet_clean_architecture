using blog__dotnet_clean_architecture.Domain.Entities;
using LogicArt.Common;
using MediatR;

namespace blog__dotnet_clean_architecture.Application.UseCases.Queries.GetTodo;

public record Query(Id Id) : IRequest<Result<Response>>;
