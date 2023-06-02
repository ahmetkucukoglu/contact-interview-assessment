using System.Net;
using System.Net.Http.Json;
using ContactApp.Report.Application.Commands.AddReportData;
using ContactApp.Report.Application.Commands.CreateReport;
using ContactApp.Report.Application.Queries.GetReport;
using ContactApp.Report.Application.Queries.GetReports;
using ContactApp.Report.Domain.Aggregates;
using ContactApp.Shared.Middlewares;
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

    [Fact, Priority(3)]
    public async void Should_ReturnReport_When_GetReport()
    {
        var response =
            await _fixture.HttpClient.GetFromJsonAsync<GetReportResponse>($"api/Reports/{_fixture.Data.ReportId}");

        Assert.NotNull(response);
        Assert.Equal(_fixture.Data.ReportId, response.Id);
        Assert.Equal(ReportStatuses.Prepared, response.Status);
        Assert.Collection(response.Data, data =>
        {
            Assert.Equal("İstanbul", data.Location);
            Assert.Equal(1, data.TotalPerson);
            Assert.Equal(2, data.TotalPhoneNumber);
        }, data =>
        {
            Assert.Equal("Antalya", data.Location);
            Assert.Equal(3, data.TotalPerson);
            Assert.Equal(4, data.TotalPhoneNumber);
        });
    }
    
    [Fact]
    public async void Should_ThrowException_If_ReportIsNotFound_When_GettingReport()
    {
        var responseMessage = await _fixture.HttpClient.GetAsync($"api/Reports/{Guid.NewGuid()}");
        var response = await responseMessage.Content.ReadFromJsonAsync<ApiErrorResponse>();
        
        Assert.Equal(HttpStatusCode.UnprocessableEntity, responseMessage.StatusCode);
        Assert.Single(response!.Errors);
    }
    
    [Fact, Priority(4)]
    public async void Should_ReturnReports_When_GetReports()
    {
        var response = await _fixture.HttpClient.GetFromJsonAsync<GetReportsResponse>("api/Reports");

        Assert.NotNull(response);
        Assert.Equal(1, response.Count);
        Assert.Collection(response.Data, data =>
        {
            Assert.Equal(_fixture.Data.ReportId, data.Id);
            Assert.Equal(ReportStatuses.Prepared, data.Status);
        });
    }

}