using GraphQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace Eudaimonia.Presentation.GraphQL;

public class GraphQLActionResult : IActionResult
{
    private readonly ExecutionResult _result;

    public GraphQLActionResult(ExecutionResult executionResult)
    {
        _result = executionResult;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var response = ResolveResponse(context);
        await WriteAsync(context, response);
    }

    private HttpResponse ResolveResponse(ActionContext context)
    {
        var response = context.HttpContext.Response;
        response.ContentType = "application/json";
        response.StatusCode = ResolveStatusCode();

        return response;
    }

    private int ResolveStatusCode()
        => _result.Executed ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest;

    private async Task WriteAsync(ActionContext context, HttpResponse response)
    {
        var serializer = context.HttpContext.RequestServices.GetRequiredService<IGraphQLSerializer>();
        await serializer.WriteAsync(response.Body, _result, context.HttpContext.RequestAborted);
    }
}