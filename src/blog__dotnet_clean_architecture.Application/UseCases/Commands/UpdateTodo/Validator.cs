using FluentValidation;

namespace blog__dotnet_clean_architecture.Application.UseCases.Commands.UpdateTodo;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Description).NotEmpty().When(x => x.IsCompleted == null);
        RuleFor(x => x.IsCompleted).NotNull().When(x => x.Description == null);
    }
}
