using Microsoft.AspNetCore.Mvc;
using PartBase.Application.Interfaces;

namespace PartBase.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManufacturersController : ControllerBase
{
    private readonly IManufacturerService _service;

    public ManufacturersController(IManufacturerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }
}