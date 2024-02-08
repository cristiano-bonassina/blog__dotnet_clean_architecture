using LogicArt.Common;
using MediatR;

namespace blog__dotnet_clean_architecture.Application.UseCases.Commands.CreateTodo;

public record Command(string Description) : IRequest<Result<Response>>;
