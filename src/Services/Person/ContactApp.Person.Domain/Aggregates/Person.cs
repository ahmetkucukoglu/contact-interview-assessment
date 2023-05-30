using System.Collections.ObjectModel;
using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Person.Domain.Aggregates;

public class Person : AggregateRoot<PersonId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public CompanyId CompanyId { get; private set; }

    private List<Contact> Contacts = new();

    private List<Contact> AddedNewContacts = new();

    public Person(string firstName, string lastName, CompanyId companyId)
    {
        ArgumentException.ThrowIfNullOrEmpty(firstName);
        ArgumentException.ThrowIfNullOrEmpty(lastName);

        Id = new PersonId(Guid.NewGuid());
        CompanyId = companyId;
        FirstName = firstName;
        LastName = lastName;
    }

    public void AddContact(Contact contact)
    {
        Contacts.Add(contact);
        
        AddNewContact(contact);
    }

    private void AddNewContact(Contact contact)
    {
        if (AddedNewContacts == null) AddedNewContacts = new();
        
        AddedNewContacts.Add(contact);
    }

    public ReadOnlyCollection<Contact> GetAddedNewContacts() => Contacts.AsReadOnly();
}