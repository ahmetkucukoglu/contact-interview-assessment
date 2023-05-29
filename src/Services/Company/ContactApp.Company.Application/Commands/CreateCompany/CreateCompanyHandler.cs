using ContactApp.Company.Domain.Repositories;
using MediatR;

namespace ContactApp.Company.Application.Commands.CreateCompany;

public class CreateCompanyHandler : IRequestHandler<CreateCompany, CreateCompanyResponse>
{
    private readonly ICompanyRepository _companyRepository;

    public CreateCompanyHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<CreateCompanyResponse> Handle(CreateCompany request, CancellationToken cancellationToken)
    {
        var company = new Domain.Aggregates.Company(request.Name);

        await _companyRepository.Create(company, cancellationToken);

        return new CreateCompanyResponse(company.Id.Value, company.Name);
    }
}