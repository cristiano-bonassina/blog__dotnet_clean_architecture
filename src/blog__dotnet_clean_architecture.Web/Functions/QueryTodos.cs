using System.Threading;
using System.Threading.Tasks;
using LogicArt.Common.AzureFunctions.Extensions;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using QueryTodosQuery = blog__dotnet_clean_architecture.Application.UseCases.Queries.QueryTodos.Query;

namespace blog__dotnet_clean_architecture.Api.Web.Functions;

public static class QueryTodos
{
    [Function(nameof(QueryTodos))]
    public static async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/todos/q")]
        HttpRequestData req,
        FunctionContext context,
        bool? isCompleted,
        int limit,
        int offset,
        CancellationToken cancellationToken)
    {
        var request = new QueryTodosQuery(
            IsCompleted: isCompleted,
            Limit: limit,
            Offset: offset);
        var mediator = context.InstanceServices.GetRequiredService<ISender>();
        var response = await mediator.Send(request, cancellationToken);

        return await req.WriteResponseAsJsonAsync(response, cancellationToken);
    }
}