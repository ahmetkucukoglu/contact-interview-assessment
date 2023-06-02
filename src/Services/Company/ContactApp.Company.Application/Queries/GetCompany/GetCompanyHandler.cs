using ContactApp.Company.Domain.Aggregates;
using ContactApp.Company.Domain.Repositories;
using ContactApp.Shared.Abstractions.DDD;
using MediatR;

namespace ContactApp.Company.Application.Queries.GetCompany;

public class GetCompanyHandler : IRequestHandler<GetCompany, GetCompanyResponse>
{
    private readonly ICompanyRepository _companyRepository;

    public GetCompanyHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<GetCompanyResponse> Handle(GetCompany request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.Get(new CompanyId(request.Id), cancellationToken);

        if (company == null) throw new DomainException("Company not found");

        return new GetCompanyResponse(company.Id.Value, company.Name);
    }
}