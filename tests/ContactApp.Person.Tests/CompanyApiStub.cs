using ContactApp.Shared.HttpServices.Company;

namespace ContactApp.Person.Tests;

public class CompanyApiStub : ICompanyApi
{
    public Task<CreateCompanyResponse> Create(CreateCompany request)
    {
        return Task.FromResult(new CreateCompanyResponse(Guid.Empty, string.Empty));
    }

    public Task<GetCompanyResponse> Get(Guid id)
    {
        return Task.FromResult(new GetCompanyResponse(id, string.Empty));
    }

    public Task<GetCompaniesResponse> GetAll()
    {
        return Task.FromResult(new GetCompaniesResponse(new List<GetCompaniesData>()));
    }
}