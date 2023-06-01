namespace ContactApp.Gateway.Middlewares;

public static class MiddlewareExtensions
{
    public static IServiceCollection AddApiExceptionHandler(this IServiceCollection serviceCollection)
        => serviceCollection.AddTransient<ApiExceptionHandler>();
    
    public static IServiceCollection AddRefitHeaderHandler(this IServiceCollection serviceCollection)
        => serviceCollection.AddTransient<RefitHeaderHandler>();

    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<ApiExceptionHandler>();
}