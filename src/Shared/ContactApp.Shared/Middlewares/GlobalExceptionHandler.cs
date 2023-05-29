using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace ContactApp.Shared.Middlewares;

public class GlobalExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ApiErrorResponse
            {
                Errors = new List<string>
                {
                    e.Message
                }
            }));
        }
    }
}