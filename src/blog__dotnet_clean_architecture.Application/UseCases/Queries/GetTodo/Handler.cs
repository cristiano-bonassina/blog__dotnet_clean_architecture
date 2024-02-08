using System.Threading;
using System.Threading.Tasks;
using blog__dotnet_clean_architecture.Application.Data.Repositories;
using blog__dotnet_clean_architecture.Domain.Entities;
using LogicArt.Common;
using MediatR;

namespace blog__dotnet_clean_architecture.Application.UseCases.Queries.GetTodo;

public class Handler : IRequestHandler<Query, Result<Response>>
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Todo> _todoRepository;

    public Handler(IMapper mapper, IReadOnlyRepository<Todo> todoRepository)
    {
        _mapper = mapper;
        _todoRepository = todoRepository;
    }

    public async Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(request.Id, cancellationToken);
        if (todo == null)
        {
            return Result.NotFound<Response>("TodoNotFound");
        }

        var response = _mapper.Map<Response>(todo);

        return Result.Success(response);
    }
}
