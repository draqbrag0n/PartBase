using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PartBase.Domain.Entities;
using PartBase.Infrastructure.Identity;

namespace PartBase.Infrastructure.Persistence;

public class PartBaseDbContext : IdentityDbContext<ApplicationUser>
{
    public PartBaseDbContext(DbContextOptions<PartBaseDbContext> options)
        : base(options)
    {
    }

    public DbSet<Component> Components => Set<Component>();
    public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PartBaseDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}