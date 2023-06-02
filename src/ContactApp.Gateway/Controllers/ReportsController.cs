using ContactApp.Shared.HttpServices.Report;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Gateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportApi _reportApi;

    public ReportsController(IReportApi reportApi)
    {
        _reportApi = reportApi;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReport request)
    {
        var result = await _reportApi.Create(request);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] GetReport request)
    {
        var result = await _reportApi.Get(request.Id);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetReports request)
    {
        var result = await _reportApi.GetAll(request.Page, request.Size);

        return Ok(result);
    }
}