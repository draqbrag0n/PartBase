namespace PartBase.Application.DTOs.Components;

public class CreateComponentRequest
{
    public string PartNumber { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid ManufacturerId { get; set; }

    public Guid CategoryId { get; set; }

    public string Package { get; set; } = string.Empty;

    public string DatasheetUrl { get; set; } = string.Empty;
}