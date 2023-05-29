using System.Net;
using System.Net.Http.Json;
using ContactApp.Company.Application.Commands.CreateCompany;
using ContactApp.Shared.Middlewares;

namespace ContactApp.Company.Tests.E2ETests;

public class E2ETests : IClassFixture<E2ETestsFixture>
{
    private readonly E2ETestsFixture _fixture;

    public E2ETests(E2ETestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async void Should_ReturnSuccess_When_CreateCompany()
    {
        var request = new CreateCompany("Rise Consulting");

        var responseMessage = await _fixture.HttpClient.PostAsJsonAsync("api/Companies", request);

        responseMessage.EnsureSuccessStatusCode();
    }

    [Fact]
    public async void Should_ThrowException_If_NameIsNull_While_CreatingCompany()
    {
        var request = new CreateCompany(string.Empty);
        
        var responseMessage = await _fixture.HttpClient.PostAsJsonAsync("api/Companies", request);

        var response = await responseMessage.Content.ReadFromJsonAsync<ApiErrorResponse>();

        Assert.Equal(HttpStatusCode.InternalServerError, responseMessage.StatusCode);
        Assert.Single(response.Errors);
    }
}