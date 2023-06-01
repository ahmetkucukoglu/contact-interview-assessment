using System.Net.Http.Json;
using ContactApp.Person.Application.Commands.AddContact;
using ContactApp.Person.Application.Commands.CreatePerson;
using ContactApp.Person.Application.Queries.GetLocationReport;
using ContactApp.Person.Domain.Aggregates;
using Xunit.Priority;

namespace ContactApp.Person.Tests.E2ETests;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[Collection("E2ETests")]
public class E2ELocationReportTests
{
    private readonly E2ETestsFixture _fixture;

    public E2ELocationReportTests(E2ETestsFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory, Priority(7)]
    [InlineData("FirstName 1", "LastName 1", "İstanbul", "5412003344")]
    [InlineData("FirstName 2", "LastName 2", "İstanbul", "5412003345")]
    [InlineData("FirstName 3", "LastName 3", "Antalya", "5412003346")]
    [InlineData("FirstName 4", "LastName 4", "", "5412003346")]
    public async void Should_ReturnSuccess_When_CreatePersonAndAddContact(string firstName, string lastName,
        string location,
        string phoneNumber)
    {
        var createPerson = new CreatePerson
        {
            FirstName = firstName,
            LastName = lastName,
            CompanyId = Guid.NewGuid()
        };

        var createPersonResponseMessage = await _fixture.HttpClient.PostAsJsonAsync("api/Persons", createPerson);
        var createPersonResponse = await createPersonResponseMessage.Content.ReadFromJsonAsync<CreatePersonResponse>();

        var addPhoneNumberContact = new AddContact
        {
            Type = ContactTypes.PhoneNumber,
            Value = phoneNumber
        };

        await _fixture.HttpClient.PutAsJsonAsync($"api/Persons/{createPersonResponse!.Id}/contacts",
            addPhoneNumberContact);

        if (string.IsNullOrEmpty(location)) return;

        var addLocationContact = new AddContact
        {
            Type = ContactTypes.Location,
            Value = location
        };

        await _fixture.HttpClient.PutAsJsonAsync($"api/Persons/{createPersonResponse!.Id}/contacts",
            addLocationContact);
    }

    [Fact, Priority(8)]
    public async void Should_ReturnSuccess_When_GetLocationReport()
    {
        var locationReportResponse = await _fixture.Mediator.Send(new GetLocationReport());

        Assert.Collection(locationReportResponse.Data, data =>
        {
            Assert.Equal(data.Location, "Antalya");
            Assert.Equal(data.TotalPerson, 1);
            Assert.Equal(data.TotalPhoneNumber, 1);
        }, data =>
        {
            Assert.Equal(data.Location, "İstanbul");
            Assert.Equal(data.TotalPerson, 2);
            Assert.Equal(data.TotalPhoneNumber, 2);
        });
    }
}