using ContactApp.Gateway.Services.Company;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Gateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyApi _companyApi;

    public CompaniesController(ICompanyApi companyApi)
    {
        _companyApi = companyApi;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompany request)
    {
        var result = await _companyApi.Create(request);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] GetCompany request)
    {
        var result = await _companyApi.Get(request.Id);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetCompanies request)
    {
        var result = await _companyApi.GetAll();

        return Ok(result);
    }
}