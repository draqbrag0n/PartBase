using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class ComponentsController : ControllerBase
{
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? q)
    {
        var result = await _service.SearchAsync(q);

        return Ok(result);
    }
}