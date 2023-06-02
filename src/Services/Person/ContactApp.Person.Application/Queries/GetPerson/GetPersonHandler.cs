using ContactApp.Person.Domain.Aggregates;
using ContactApp.Person.Domain.Repositories;
using ContactApp.Shared.Abstractions.DDD;
using ContactApp.Shared.HttpServices.Company;
using MediatR;

namespace ContactApp.Person.Application.Queries.GetPerson;

public class GetPersonHandler : IRequestHandler<GetPerson, GetPersonResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly ICompanyApi _companyApi;

    public GetPersonHandler(IPersonRepository personRepository, ICompanyApi companyApi)
    {
        _personRepository = personRepository;
        _companyApi = companyApi;
    }

    public async Task<GetPersonResponse> Handle(GetPerson request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.Get(new PersonId(request.Id), cancellationToken);

        if (person == null) throw new DomainException("Person not found");

        var company = await _companyApi.Get(person.CompanyId.Value);

        return new GetPersonResponse
        {
            Id = person.Id.Value,
            FirstName = person.FirstName,
            LastName = person.LastName,
            CompanyId = person.CompanyId.Value,
            CompanyName = company.Name,
            Contacts = person.GetContacts().Select(c => new GetPersonContact
            {
                Id = c.Id,
                Type = c.Type,
                Value = c.Value
            })
        };
    }
}