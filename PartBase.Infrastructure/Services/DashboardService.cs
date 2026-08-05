using Microsoft.EntityFrameworkCore;
using PartBase.Application.DTOs.Components;
using PartBase.Application.DTOs.Dashboard;
using PartBase.Application.Interfaces;
using PartBase.Infrastructure.Persistence;

namespace PartBase.Infrastructure.Services;

public class DashboardService : IDashboardService
{
    private readonly PartBaseDbContext _context;

    public DashboardService(PartBaseDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetDashboardAsync()
    {
        return new DashboardDto
        {
            TotalComponents = await _context.Components.CountAsync(),

            TotalManufacturers = await _context.Manufacturers.CountAsync(),

            TotalCategories = await _context.Categories.CountAsync(),

            LatestComponents = await _context.Components
                .Include(x => x.Manufacturer)
                .Include(x => x.Category)
                .OrderByDescending(x => x.CreatedAt)
                .Take(5)
                .Select(x => new ComponentDto
                {
                    Id = x.Id,
                    PartNumber = x.PartNumber,
                    Description = x.Description,
                    Manufacturer = x.Manufacturer.Name,
                    Category = x.Category.Name,
                    Package = x.Package,
                    DatasheetUrl = x.DatasheetUrl
                })
                .ToListAsync()
        };
    }
}