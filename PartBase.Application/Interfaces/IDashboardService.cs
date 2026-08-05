using PartBase.Application.DTOs.Dashboard;

namespace PartBase.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync();
}