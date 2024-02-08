using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using blog__dotnet_clean_architecture.Application.Data.Repositories;
using blog__dotnet_clean_architecture.Domain.Entities;
using LogicArt.Common;
using LogicArt.Common.EntityFramework.Extensions;
using LogicArt.Common.Extensions;
using MediatR;

namespace blog__dotnet_clean_architecture.Application.UseCases.Queries.QueryTodos;

public class Handler : IRequestHandler<Query, Result<PageResult<Response>>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Todo> _todoRepository;

    public Handler(IMapper mapper, IReadOnlyRepository<Todo> todoRepository)
    {
        _mapper = mapper;
        _todoRepository = todoRepository;
    }

    public async Task<Result<PageResult<Response>>> Handle(Query request, CancellationToken cancellationToken)
    {
        var todos = await _todoRepository.Query()
            .WhereWhen(request.IsCompleted.HasValue, x => x.IsCompleted == request.IsCompleted)
            .OrderByDescending(x => x.CreatedAt)
            .PaginateAsync(request.Limit, request.Offset, cancellationToken);

        var response = todos.ToPageResult<Response>(_mapper);

        return Result.Success(response);
    }
}