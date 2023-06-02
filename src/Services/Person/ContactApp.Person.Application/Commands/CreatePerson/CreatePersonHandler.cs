using ContactApp.Person.Domain.Aggregates;
using ContactApp.Person.Domain.Repositories;
using ContactApp.Shared.HttpServices.Company;
using MediatR;

namespace ContactApp.Person.Application.Commands.CreatePerson;

public class CreatePersonHandler : IRequestHandler<CreatePerson, CreatePersonResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly ICompanyApi _companyApi;

    public CreatePersonHandler(IPersonRepository personRepository, ICompanyApi companyApi)
    {
        _personRepository = personRepository;
        _companyApi = companyApi;
    }

    public async Task<CreatePersonResponse> Handle(CreatePerson request, CancellationToken cancellationToken)
    {
        var company = await _companyApi.Get(request.CompanyId);
        
        var person = new Domain.Aggregates.Person(
            request.FirstName, request.LastName, new CompanyId(company.Id));

        await _personRepository.Create(person, cancellationToken);

        return new CreatePersonResponse
        {
            Id = person.Id.Value,
            FirstName = person.FirstName,
            LastName = person.LastName,
            CompanyId = person.CompanyId.Value
        };
    }
}