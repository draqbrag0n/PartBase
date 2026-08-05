namespace PartBase.Application.DTOs.Components;

public class ComponentDto
{
    public Guid Id { get; set; }

    public string PartNumber { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid ManufacturerId { get; set; }

    public string Manufacturer { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }

    public string Category { get; set; } = string.Empty;

    public string Package { get; set; } = string.Empty;

    public string DatasheetUrl { get; set; } = string.Empty;
}