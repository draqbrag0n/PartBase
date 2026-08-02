using Microsoft.EntityFrameworkCore;
using PartBase.Application.Interfaces;
using PartBase.Domain.Entities;
using PartBase.Infrastructure.Persistence;

namespace PartBase.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly PartBaseDbContext _context;

    public CategoryService(PartBaseDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
}