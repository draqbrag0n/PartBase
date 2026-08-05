using PartBase.Application.DTOs.Components;

namespace PartBase.Application.DTOs.Dashboard;

public class DashboardDto
{
    public int TotalComponents { get; set; }

    public int TotalManufacturers { get; set; }

    public int TotalCategories { get; set; }

    public List<ComponentDto> LatestComponents { get; set; } = new();
}