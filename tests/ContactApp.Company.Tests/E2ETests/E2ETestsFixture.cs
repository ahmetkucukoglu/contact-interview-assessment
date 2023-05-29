using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ContactApp.Company.Tests.E2ETests;

public class E2ETestsFixture : IDisposable
{
    private readonly Application _application;
    public readonly HttpClient HttpClient;
    
    public E2ETestsFixture()
    {
        _application = new Application();

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