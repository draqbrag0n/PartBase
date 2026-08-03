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

    /// <summary>
    /// Komponentleri listeler.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _service.GetAllAsync(search, page, pageSize);

        return Ok(result);
    }

    /// <summary>
    /// Id'ye göre komponent getirir.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var component = await _service.GetByIdAsync(id);

        if (component is null)
            return NotFound();

        return Ok(component);
    }

    /// <summary>
    /// Yeni komponent oluşturur.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateComponentRequest request)
    {
        var component = await _service.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = component.Id },
            component);
    }

    /// <summary>
    /// Komponenti günceller.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(
        Guid id,
        [FromBody] CreateComponentRequest request)
    {
        var updated = await _service.UpdateAsync(id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Komponenti siler.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}