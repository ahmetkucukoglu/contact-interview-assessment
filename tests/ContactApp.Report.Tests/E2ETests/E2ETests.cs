using System.Net.Http.Json;
using ContactApp.Company.Tests.E2ETests;
using ContactApp.Report.Application.Commands.CreateReport;
using ContactApp.Report.Domain.Aggregates;
using Xunit.Priority;

namespace ContactApp.Report.Tests.E2ETests;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class E2ETests : IClassFixture<E2ETestsFixture>
{
    private readonly E2ETestsFixture _fixture;

    public E2ETests(E2ETestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact, Priority(1)]
    public async void Should_ReturnSuccess_When_CreateReport()
    {
        var request = new CreateReport();
        var responseMessage = await _fixture.HttpClient.PostAsJsonAsync("api/Reports", request);

        responseMessage.EnsureSuccessStatusCode();

        var response = await responseMessage.Content.ReadFromJsonAsync<CreateReportResponse>();
        
        Assert.NotNull(response);
        Assert.Equal(ReportStatuses.Preparing, response.Status);
        
        _fixture.Data.ReportId = response!.Id;
    }
}