using Microsoft.EntityFrameworkCore;
using PartBase.Application.Interfaces;
using PartBase.Domain.Entities;
using PartBase.Infrastructure.Persistence;

namespace PartBase.Infrastructure.Services;

public class ManufacturerService : IManufacturerService
{
    private readonly PartBaseDbContext _context;

    public ManufacturerService(PartBaseDbContext context)
    {
        _context = context;
    }

    public async Task<List<Manufacturer>> GetAllAsync()
    {
        return await _context.Manufacturers
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
}