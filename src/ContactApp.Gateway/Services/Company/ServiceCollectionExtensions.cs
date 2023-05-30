using Refit;

namespace ContactApp.Gateway.Services.Company;

public static class ServiceCollectionExtensions
{
    public static void AddCompanies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<ICompanyApi>()
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration.GetRequiredSection("Services:CompanyBaseUri").Value!));
    }
}