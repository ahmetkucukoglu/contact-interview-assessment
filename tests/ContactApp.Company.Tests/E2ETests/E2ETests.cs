using System.Net;
using System.Net.Http.Json;
using ContactApp.Company.Application.Commands.CreateCompany;
using ContactApp.Company.Application.Queries.GetCompanies;
using ContactApp.Company.Application.Queries.GetCompany;
using ContactApp.Shared.Middlewares;
using Xunit.Priority;

namespace ContactApp.Company.Tests.E2ETests;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class E2ETests : IClassFixture<E2ETestsFixture>
{
    private readonly E2ETestsFixture _fixture;

    public E2ETests(E2ETestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact, Priority(1)]
    public async void Should_ReturnSuccess_When_CreateCompany()
    {
        _fixture.Data.CompanyName = "Rise Consulting";

        var request = new CreateCompany(_fixture.Data.CompanyName);
        var responseMessage = await _fixture.HttpClient.PostAsJsonAsync("api/Companies", request);

        responseMessage.EnsureSuccessStatusCode();

        var response = await responseMessage.Content.ReadFromJsonAsync<CreateCompanyResponse>();
        _fixture.Data.CompanyId = response!.Id;
    }

    [Fact]
    public async void Should_ThrowException_If_NameIsNull_While_CreatingCompany()
    {
        var request = new CreateCompany(string.Empty);
        var responseMessage = await _fixture.HttpClient.PostAsJsonAsync("api/Companies", request);
        var response = await responseMessage.Content.ReadFromJsonAsync<ApiErrorResponse>();

        Assert.Equal(HttpStatusCode.InternalServerError, responseMessage.StatusCode);
        Assert.Single(response!.Errors);
    }

    [Fact, Priority(2)]
    public async void Should_ReturnCompany_When_GetCompany()
    {
        var response =
            await _fixture.HttpClient.GetFromJsonAsync<GetCompanyResponse>($"api/Companies/{_fixture.Data.CompanyId}");

        Assert.NotNull(response);
        Assert.Equal(_fixture.Data.CompanyName, response.Name);
        Assert.Equal(_fixture.Data.CompanyId, response.Id);
    }

    [Fact, Priority(3)]
    public async void Should_ReturnCompanies_When_GetCompanies()
    {
        var response = await _fixture.HttpClient.GetFromJsonAsync<GetCompaniesResponse>("api/Companies");

        Assert.NotNull(response);
        Assert.Collection(response.Companies, data =>
        {
            Assert.Equal(_fixture.Data.CompanyId, data.Id);
            Assert.Equal(_fixture.Data.CompanyName, data.Name);
        });
    }
}