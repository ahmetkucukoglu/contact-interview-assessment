using System.Net;
using System.Net.Http.Json;
using ContactApp.Person.Application.Commands.CreatePerson;
using ContactApp.Shared.Middlewares;
using Xunit.Priority;

namespace ContactApp.Person.Tests.E2ETests;

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
        _fixture.Data.FirstName = "Ahmet";
        _fixture.Data.LastName = "KÜÇÜKOĞLU";
        _fixture.Data.CompanyId = Guid.NewGuid();

        var request = new CreatePerson
        {
            FirstName = _fixture.Data.FirstName,
            LastName = _fixture.Data.LastName,
            CompanyId = _fixture.Data.CompanyId
        };
        var responseMessage = await _fixture.HttpClient.PostAsJsonAsync("api/Persons", request);

        responseMessage.EnsureSuccessStatusCode();

        var response = await responseMessage.Content.ReadFromJsonAsync<CreatePersonResponse>();
        _fixture.Data.PersonId = response!.Id;
    }

    [Fact]
    public async void Should_ThrowException_If_FirstNameIsNull_While_CreatingCompany()
    {
        var request = new CreatePerson
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            CompanyId = Guid.NewGuid()
        };
        var responseMessage = await _fixture.HttpClient.PostAsJsonAsync("api/Persons", request);
        var response = await responseMessage.Content.ReadFromJsonAsync<ApiErrorResponse>();

        Assert.Equal(HttpStatusCode.InternalServerError, responseMessage.StatusCode);
        Assert.Single(response!.Errors);
    }
}