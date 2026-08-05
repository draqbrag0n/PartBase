using Microsoft.AspNetCore.Mvc;
using PartBase.Application.Interfaces;

namespace PartBase.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _service;

    public DashboardController(IDashboardService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetDashboardAsync());
    }
}