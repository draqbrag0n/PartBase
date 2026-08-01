using Microsoft.EntityFrameworkCore;
using PartBase.Domain.Entities;

namespace PartBase.Infrastructure.Persistence;

public class PartBaseDbContext : DbContext
{
    public PartBaseDbContext(DbContextOptions<PartBaseDbContext> options)
        : base(options)
    {
    }

    public DbSet<Component> Components => Set<Component>();
}