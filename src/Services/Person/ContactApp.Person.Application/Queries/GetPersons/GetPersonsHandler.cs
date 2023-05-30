using ContactApp.Person.Domain.Repositories;
using MediatR;

namespace ContactApp.Person.Application.Queries.GetPersons;

public class GetPersonsHandler : IRequestHandler<GetPersons, GetPersonsResponse>
{
    private readonly IPersonRepository _personRepository;

    public GetPersonsHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<GetPersonsResponse> Handle(GetPersons request, CancellationToken cancellationToken)
    {
        var (count,persons) = await _personRepository.GetAll(request.Page, request.Size, cancellationToken);
        var responseData = persons.Select(p => new GetPersonsData
        {
            Id = p.Id.Value,
            FirstName = p.FirstName,
            LastName = p.LastName
        });

        return new GetPersonsResponse(count, responseData);
    }
}