using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LogicArt.Common.AzureFunctions.Extensions;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using UpdateTodoCommand = blog__dotnet_clean_architecture.Application.UseCases.Commands.UpdateTodo.Command;

namespace blog__dotnet_clean_architecture.Api.Web.Functions;

public static class UpdateTodo
{
    [Function(nameof(UpdateTodo))]
    public static async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/todos/{id:guid}")]
        HttpRequestData req,
        FunctionContext context,
        Guid id,
        CancellationToken cancellationToken)
    {
        var request = await req.ReadFromJsonAsync<UpdateTodoCommand>(cancellationToken);
        if (request == null)
        {
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        var mediator = context.InstanceServices.GetRequiredService<ISender>();
        var response = await mediator.Send(request with { Id = id }, cancellationToken);
        if (response.IsSuccess)
        {
            return req.CreateResponse(HttpStatusCode.NoContent);
        }

        return await req.WriteResponseAsJsonAsync(response, cancellationToken);
    }
}