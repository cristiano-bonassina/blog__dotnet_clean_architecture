using blog__dotnet_clean_architecture.Domain.Entities;
using Mapster;
using UpdateTodoCommand = blog__dotnet_clean_architecture.Application.UseCases.Commands.UpdateTodo.Command;

namespace blog__dotnet_clean_architecture.Application.Data.Mapping;

public class MappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<string, Description>()
            .MapWith(x => new Description(x));

        config.NewConfig<UpdateTodoCommand, Todo>()
            .IgnoreNullValues(true);
    }
}
