using Microsoft.AspNetCore.Mvc;
using PartBase.Application.DTOs.Components;
using PartBase.Application.Interfaces;

namespace PartBase.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComponentsController : ControllerBase
{
    private readonly IComponentService _service;

    public ComponentsController(IComponentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        string? q,
        int page = 1,
        int pageSize = 20)
    {
        var result = await _service.GetAllAsync(q, page, pageSize);

        return Ok(result);
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var component = await _service.GetByIdAsync(id);

        if (component == null)
            return NotFound();

        return Ok(component);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateComponentRequest request)
    {
        var component = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = component.Id },
            component);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, CreateComponentRequest request)
    {
        var updated = await _service.UpdateAsync(id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}