using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LogicArt.Common.AzureFunctions.Extensions;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using CreateTodoCommand = blog__dotnet_clean_architecture.Application.UseCases.Commands.CreateTodo.Command;

namespace blog__dotnet_clean_architecture.Api.Web.Functions;

public static class CreateTodo
{
    [Function(nameof(CreateTodo))]
    public static async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/todos")]
        HttpRequestData req,
        FunctionContext context,
        CancellationToken cancellationToken)
    {
        var request = await req.ReadFromJsonAsync<CreateTodoCommand>(cancellationToken);
        if (request == null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        var mediator = context.InstanceServices.GetRequiredService<ISender>();
        var response = await mediator.Send(request, cancellationToken);

        return await req.WriteResponseAsJsonAsync(response, HttpStatusCode.Created, cancellationToken);
    }
}