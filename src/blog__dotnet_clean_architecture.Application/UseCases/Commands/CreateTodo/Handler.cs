using System.Threading;
using System.Threading.Tasks;
using blog__dotnet_clean_architecture.Application.Data;
using blog__dotnet_clean_architecture.Application.Data.Repositories;
using blog__dotnet_clean_architecture.Domain.Entities;
using LogicArt.Common;
using MediatR;

namespace blog__dotnet_clean_architecture.Application.UseCases.Commands.CreateTodo;

public class Handler : IRequestHandler<Command, Result<Response>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Todo> _todoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public Handler(
        IMapper mapper,
        IRepository<Todo> todoRepository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _todoRepository = todoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var todo = _mapper.Map<Todo>(request);
        _todoRepository.Add(todo);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(new Response(Id: todo.Id));
    }
}
