using ContactApp.Company.Domain.Repositories;
using MediatR;

namespace ContactApp.Company.Application.Queries.GetCompanies;

public class GetCompaniesHandler : IRequestHandler<GetCompanies, GetCompaniesResponse>
{
    private readonly ICompanyRepository _companyRepository;

    public GetCompaniesHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<GetCompaniesResponse> Handle(GetCompanies request, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetAll(cancellationToken);
        var responseData = companies.Select(c => new GetCompaniesData(c.Id.Value, c.Name));
        
        return new GetCompaniesResponse(responseData);
    }
}