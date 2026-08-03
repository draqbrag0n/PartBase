namespace PartBase.Application.DTOs.Components;

public class ComponentSearchRequest
{
    public string? Search { get; set; }

    public Guid? ManufacturerId { get; set; }

    public Guid? CategoryId { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}