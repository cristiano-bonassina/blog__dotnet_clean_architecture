using System.Threading;
using System.Threading.Tasks;
using blog__dotnet_clean_architecture.Application.Data;
using blog__dotnet_clean_architecture.Application.Data.Repositories;
using blog__dotnet_clean_architecture.Domain.Entities;
using LogicArt.Common;
using MediatR;

namespace blog__dotnet_clean_architecture.Application.UseCases.Commands.DeleteTodo;

public class Handler : IRequestHandler<Command, Result<Response>>
{
    private readonly IRepository<Todo> _todoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public Handler(IRepository<Todo> todoRepository, IUnitOfWork unitOfWork)
    {
        _todoRepository = todoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var todo = await _todoRepository.GetByIdAsync(request.Id, cancellationToken);
        if (todo == null)
        {
            return Result.NotFound<Response>("TodoNotFound");
        }

        _todoRepository.Remove(todo);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(new Response());
    }
}
