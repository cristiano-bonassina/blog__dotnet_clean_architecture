using FluentValidation;

namespace blog__dotnet_clean_architecture.Application.UseCases.Commands.CreateTodo;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Description).NotEmpty();
    }
}
