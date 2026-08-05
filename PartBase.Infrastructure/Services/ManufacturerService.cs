using Microsoft.EntityFrameworkCore;
using PartBase.Application.Common;
using PartBase.Application.Interfaces;
using PartBase.Infrastructure.Persistence;

namespace PartBase.Infrastructure.Services;

public class ManufacturerService : IManufacturerService
{
    private readonly PartBaseDbContext _context;

    public ManufacturerService(PartBaseDbContext context)
    {
        _context = context;
    }

    public async Task<List<LookupDto>> GetAllAsync()
    {
        return await _context.Manufacturers
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new LookupDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }
}