using Refit;

namespace ContactApp.Gateway.Services.Company;

public interface ICompanyApi
{
    [Post("/companies")]
    Task<CreateCompanyResponse> Create([Body]CreateCompany request);
    
    [Get("/companies/{id}")]
    Task<GetCompanyResponse> Get(Guid id);
    
    [Get("/companies")]
    Task<GetCompaniesResponse> GetAll();
}