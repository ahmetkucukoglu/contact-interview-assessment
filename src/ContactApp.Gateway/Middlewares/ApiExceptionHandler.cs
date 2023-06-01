using System.Net;
using Refit;

namespace ContactApp.Gateway.Middlewares;

public class ApiExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e) when (e is ApiException apiException)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(apiException.GetContentAsAsync<ApiErrorResponse>().Result);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new ApiErrorResponse {Errors = new List<string> {"Unhandled exception"}});
        }
    }
}