using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ContactApp.Report.Tests.E2ETests;

class Application : WebApplicationFactory<Program>
{
    public string ConnectionString { get; } = "mongodb://wSvTgSPx4YNhrSyP:heF2wk4JysLWT5h3@localhost:2244/";
    public string DatabaseName { get; } = $"company-tdd-{Guid.NewGuid()}";

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

        return base.CreateHost(builder);
    }
}