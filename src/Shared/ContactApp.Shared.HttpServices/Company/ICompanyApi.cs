using Refit;

namespace ContactApp.Shared.HttpServices.Company;

public interface ICompanyApi
{
    [Post("/companies")]
    Task<CreateCompanyResponse> Create([Body]CreateCompany request);
    
    [Get("/companies/{id}")]
    Task<GetCompanyResponse> Get(Guid id);
    
    [Get("/companies")]
    Task<GetCompaniesResponse> GetAll();
}