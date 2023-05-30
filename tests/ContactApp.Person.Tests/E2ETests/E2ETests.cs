using System.Net;
using System.Net.Http.Json;
using ContactApp.Person.Application.Commands.AddContact;
using ContactApp.Person.Application.Commands.CreatePerson;
using ContactApp.Person.Application.Queries.GetPerson;
using ContactApp.Person.Domain.Aggregates;
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
    public async void Should_ReturnSuccess_When_CreatePerson()
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
    public async void Should_ThrowException_If_FirstNameIsNull_While_CreatingPerson()
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

    [Theory, Priority(2)]
    [InlineData(ContactTypes.EmailAddress, "ahmetkucukoglu@gmail.com")]
    [InlineData(ContactTypes.PhoneNumber, "5413456787")]
    [InlineData(ContactTypes.Location, "İstanbul")]
    public async void Should_ReturnSuccess_When_AddContact(ContactTypes type, string value)
    {
        var request = new AddContact
        {
            PersonId = _fixture.Data.PersonId,
            Type = type,
            Value = value
        };
        var responseMessage =
            await _fixture.HttpClient.PutAsJsonAsync($"api/Persons/{_fixture.Data.PersonId}/contacts", request);

        responseMessage.EnsureSuccessStatusCode();

        var response = await responseMessage.Content.ReadFromJsonAsync<AddContactResponse>();
        _fixture.Data.ContactId = response!.Id;
    }

    [Fact, Priority(3)]
    public async void Should_ReturnSuccess_When_RemoveContact()
    {
        var responseMessage =
            await _fixture.HttpClient.DeleteAsync(
                $"api/Persons/{_fixture.Data.PersonId}/contacts/{_fixture.Data.ContactId!.Value}");

        responseMessage.EnsureSuccessStatusCode();
    }

    [Fact, Priority(4)]
    public async void Should_ReturnPerson_When_GetPerson()
    {
        var response =
            await _fixture.HttpClient.GetFromJsonAsync<GetPersonResponse>($"api/Persons/{_fixture.Data.PersonId}");

        Assert.NotNull(response);
        Assert.Equal(_fixture.Data.PersonId, response.Id);
        Assert.Equal(_fixture.Data.FirstName, response.FirstName);
        Assert.Equal(_fixture.Data.LastName, response.LastName);
        Assert.Equal(_fixture.Data.CompanyId, response.CompanyId);
        Assert.Collection(response.Contacts, contact =>
            {
                Assert.Equal(ContactTypes.EmailAddress, contact.Type);
                Assert.Equal("ahmetkucukoglu@gmail.com", contact.Value);
            },
            contact =>
            {
                Assert.Equal(ContactTypes.PhoneNumber, contact.Type);
                Assert.Equal("5413456787", contact.Value);
            });
    }

    [Fact, Priority(5)]
    public async void Should_ReturnSuccess_When_DeletePerson()
    {
        var responseMessage =
            await _fixture.HttpClient.DeleteAsync(
                $"api/Persons/{_fixture.Data.PersonId}");

        responseMessage.EnsureSuccessStatusCode();
    }
}