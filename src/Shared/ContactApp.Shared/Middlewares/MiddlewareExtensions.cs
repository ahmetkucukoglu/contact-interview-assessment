using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Shared.Middlewares;

public static class MiddlewareExtensions
{
    public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection serviceCollection)
        => serviceCollection.AddTransient<GlobalExceptionHandler>();

    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<GlobalExceptionHandler>();
}