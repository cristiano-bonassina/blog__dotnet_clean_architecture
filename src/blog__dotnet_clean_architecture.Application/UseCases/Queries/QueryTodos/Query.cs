using LogicArt.Common;
using MediatR;

namespace blog__dotnet_clean_architecture.Application.UseCases.Queries.QueryTodos;

public record Query(
    bool? IsCompleted,
    int Limit,
    int Offset) : IRequest<Result<PageResult<Response>>>;
