using Refit;

namespace ContactApp.Gateway.Services.Report;

public interface IReportApi
{
    [Post("/reports")]
    Task<CreateReportResponse> Create([Body]CreateReport request);
    
    [Get("/reports/{id}")]
    Task<GetReportResponse> Get(Guid id);
    
    [Get("/reports")]
    Task<GetReportsResponse> GetAll(int page, int size);
}