using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Shared.HttpServices.Middlewares;

public static class MiddlewareExtensions
{
    public static IServiceCollection AddRefitHeaderHandler(this IServiceCollection serviceCollection)
        => serviceCollection.AddTransient<RefitHeaderHandler>();
}