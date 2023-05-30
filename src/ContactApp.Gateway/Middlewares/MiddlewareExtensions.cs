namespace ContactApp.Gateway.Middlewares;

public static class MiddlewareExtensions
{
    public static IServiceCollection AddApiExceptionHandler(this IServiceCollection serviceCollection)
        => serviceCollection.AddTransient<ApiExceptionHandler>();

    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<ApiExceptionHandler>();
}