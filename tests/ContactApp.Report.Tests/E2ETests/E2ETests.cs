using System.Net.Http.Json;
using ContactApp.Report.Application.Commands.AddReportData;
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
        var request = new CreateReport(Guid.NewGuid().ToString());
        var responseMessage = await _fixture.HttpClient.PostAsJsonAsync("api/Reports", request);

        responseMessage.EnsureSuccessStatusCode();

        var response = await responseMessage.Content.ReadFromJsonAsync<CreateReportResponse>();

        Assert.NotNull(response);
        Assert.Equal(ReportStatuses.Preparing, response.Status);

        _fixture.Data.ReportId = response.Id;
    }

    [Fact, Priority(2)]
    public async void Should_ReturnSuccess_When_AddReportData()
    {
        var data = new List<AddReportDataData>
        {
            new("İstanbul", 1, 2),
            new("Antalya", 3, 4)
        };

        await _fixture.Mediator.Send(new AddReportData(_fixture.Data.ReportId, data));
    }
}