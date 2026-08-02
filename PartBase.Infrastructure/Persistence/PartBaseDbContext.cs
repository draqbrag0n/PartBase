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

    public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();

    public DbSet<Category> Categories => Set<Category>();
}