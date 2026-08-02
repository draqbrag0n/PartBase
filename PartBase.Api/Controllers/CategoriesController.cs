using Microsoft.AspNetCore.Mvc;
using PartBase.Application.Interfaces;

namespace PartBase.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await _service.GetAllAsync();

        return Ok(categories);
    }
}