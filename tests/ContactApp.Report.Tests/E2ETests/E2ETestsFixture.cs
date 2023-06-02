using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ContactApp.Report.Tests.E2ETests;

public class E2ETestsFixture : IDisposable
{
    private readonly Application _application;
    public readonly IMediator Mediator;
    public readonly HttpClient HttpClient;

    public E2ETestFixtureData Data { get; set; } = new();

    public E2ETestsFixture()
    {
        _application = new Application();

        var scope = _application.Services.CreateScope();
        Mediator = scope.ServiceProvider.GetService<IMediator>()!;

        HttpClient = _application.CreateClient();
    }

    public void Dispose()
    {
        HttpClient.Dispose();

        DropDatabase();

        _application.Dispose();
    }

    private void DropDatabase()
    {
        var client = new MongoClient(_application.ConnectionString);
        client.DropDatabase(_application.DatabaseName);
    }
}