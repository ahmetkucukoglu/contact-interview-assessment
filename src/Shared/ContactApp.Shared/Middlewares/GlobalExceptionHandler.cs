using System.Net;
using System.Text.Json;
using ContactApp.Shared.Abstractions.DDD;
using Microsoft.AspNetCore.Http;
using Refit;

namespace ContactApp.Shared.Middlewares;

public class GlobalExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e) when (e is DomainException domainException)
        {
            context.Response.StatusCode = (int) HttpStatusCode.UnprocessableEntity;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ApiErrorResponse
            {
                Errors = new List<string>
                {
                    domainException.Message
                }
            }));
        }
        catch (Exception e) when (e is ApiException apiException)
        {
            var statusCode = (int) HttpStatusCode.InternalServerError;
            var errors = new List<string> {"Unhandled exception"};

            if (apiException.StatusCode == HttpStatusCode.UnprocessableEntity)
            {
                statusCode = (int) HttpStatusCode.UnprocessableEntity;

                var response = await apiException.GetContentAsAsync<ApiErrorResponse>();
                errors = response!.Errors;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ApiErrorResponse
            {
                Errors = errors
            }));
        }
        catch
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ApiErrorResponse
            {
                Errors = new List<string>
                {
                    "Unhandled exception"
                }
            }));
        }
    }
}