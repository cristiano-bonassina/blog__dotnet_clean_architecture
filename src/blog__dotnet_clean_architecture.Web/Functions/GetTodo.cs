using System;
using System.Threading;
using System.Threading.Tasks;
using LogicArt.Common.AzureFunctions.Extensions;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using GetTodoQuery = blog__dotnet_clean_architecture.Application.UseCases.Queries.GetTodo.Query;

namespace blog__dotnet_clean_architecture.Api.Web.Functions;

public static class GetTodo
{
    [Function(nameof(GetTodo))]
    public static async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/todos/{id:guid}")]
        HttpRequestData req,
        FunctionContext context,
        Guid id,
        CancellationToken cancellationToken)
    {
        var request = new GetTodoQuery(Id: id);
        var mediator = context.InstanceServices.GetRequiredService<ISender>();
        var response = await mediator.Send(request, cancellationToken);

        return await req.WriteResponseAsJsonAsync(response, cancellationToken);
    }
}