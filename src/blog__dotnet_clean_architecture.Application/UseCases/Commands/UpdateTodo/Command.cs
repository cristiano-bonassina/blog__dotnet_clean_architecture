using blog__dotnet_clean_architecture.Domain.Entities;
using LogicArt.Common;
using MediatR;

namespace blog__dotnet_clean_architecture.Application.UseCases.Commands.UpdateTodo;

public record Command(
    Id Id,
    string? Description,
    bool? IsCompleted) : IRequest<Result<Response>>;
