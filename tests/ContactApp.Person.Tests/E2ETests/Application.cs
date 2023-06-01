using ContactApp.Shared.HttpServices.Company;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactApp.Person.Tests.E2ETests;

class Application : WebApplicationFactory<Program>
{
    public string ConnectionString { get; } = "mongodb://wSvTgSPx4YNhrSyP:heF2wk4JysLWT5h3@localhost:2244/";
    public string DatabaseName { get; } = $"person-tdd-{Guid.NewGuid()}";

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            config.AddConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"MongoDbSettings:ConnectionString", ConnectionString},
                    {"MongoDbSettings:DatabaseName", DatabaseName}
                }!)
                .Build());
        });

        builder.ConfigureServices(collection => { collection.AddTransient<ICompanyApi, CompanyApiStub>(); });

        return base.CreateHost(builder);
    }
}