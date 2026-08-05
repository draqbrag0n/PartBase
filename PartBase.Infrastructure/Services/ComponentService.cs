using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PartBase.Application.Common;
using PartBase.Application.DTOs.Components;
using PartBase.Application.Interfaces;
using PartBase.Domain.Entities;
using PartBase.Infrastructure.Persistence;

namespace PartBase.Infrastructure.Services;

public class ComponentService : IComponentService
{
    private readonly PartBaseDbContext _context;
    private readonly ILogger<ComponentService> _logger;

    public ComponentService(
        PartBaseDbContext context,
        ILogger<ComponentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PagedResult<ComponentDto>> GetAllAsync(string? search,int page,int pageSize)
    {
        page = page < 1 ? 1 : page;
        pageSize = pageSize < 1 ? 20 : pageSize;
        pageSize = pageSize > 100 ? 100 : pageSize;

        _logger.LogInformation(
        "Listing components. Search: {Search}, Page: {Page}, PageSize: {PageSize}",
        search,
        page,
        pageSize);

        var query = _context.Components
            .AsNoTracking()
            .Include(x => x.Manufacturer)
            .Include(x => x.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.PartNumber.Contains(search) ||
                x.Description.Contains(search));
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.PartNumber)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ComponentDto
            {
                Id = x.Id,
                PartNumber = x.PartNumber,
                Description = x.Description,

                ManufacturerId = x.ManufacturerId,
                Manufacturer = x.Manufacturer.Name,

                CategoryId = x.CategoryId,
                Category = x.Category.Name,

                Package = x.Package,
                DatasheetUrl = x.DatasheetUrl
            })
            .ToListAsync();

        return new PagedResult<ComponentDto>
        {
            Items = items,
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<ComponentDto?> GetByIdAsync(Guid id)
    {
        return await _context.Components
            .AsNoTracking()
            .Include(x => x.Manufacturer)
            .Include(x => x.Category)
            .Where(x => x.Id == id)
            .Select(x => new ComponentDto
            {
                Id = x.Id,
                PartNumber = x.PartNumber,
                Description = x.Description,

                ManufacturerId = x.ManufacturerId,
                Manufacturer = x.Manufacturer.Name,

                CategoryId = x.CategoryId,
                Category = x.Category.Name,

                Package = x.Package,
                DatasheetUrl = x.DatasheetUrl
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ComponentDto> CreateAsync(CreateComponentRequest request)
    {
        _logger.LogInformation("Creating component {PartNumber}", request.PartNumber);

        var component = new Component(
            request.PartNumber,
            request.Description,
            request.ManufacturerId,
            request.CategoryId,
            request.Package,
            request.DatasheetUrl);

        _context.Components.Add(component);

        await _context.SaveChangesAsync();

        return new ComponentDto
        {
            Id = component.Id,
            PartNumber = component.PartNumber,
            Description = component.Description,
            Package = component.Package,
            DatasheetUrl = component.DatasheetUrl
        };
    }

    public async Task<bool> UpdateAsync(Guid id, CreateComponentRequest request)
    {
        var component = await _context.Components.FindAsync(id);

        if (component is null)
            return false;

        component.Update(
            request.PartNumber,
            request.Description,
            request.ManufacturerId,
            request.CategoryId,
            request.Package,
            request.DatasheetUrl);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var component = await _context.Components.FindAsync(id);

        if (component is null)
            return false;

        _context.Components.Remove(component);

        await _context.SaveChangesAsync();

        return true;
    }
}