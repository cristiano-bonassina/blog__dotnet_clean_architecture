using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LogicArt.Common.AzureFunctions.Extensions;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using DeleteTodoCommand = blog__dotnet_clean_architecture.Application.UseCases.Commands.DeleteTodo.Command;

namespace blog__dotnet_clean_architecture.Api.Web.Functions;

public static class DeleteTodo
{
    [Function(nameof(DeleteTodo))]
    public static async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/todos/{id:guid}")]
        HttpRequestData req,
        FunctionContext context,
        Guid id,
        CancellationToken cancellationToken)
    {
        var request = new DeleteTodoCommand(Id: id);
        var mediator = context.InstanceServices.GetRequiredService<ISender>();
        var response = await mediator.Send(request, cancellationToken);
        if (response.IsSuccess)
        {
            return req.CreateResponse(HttpStatusCode.NoContent);
        }

        return await req.WriteResponseAsJsonAsync(response, cancellationToken);
    }
}