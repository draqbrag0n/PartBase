using Microsoft.EntityFrameworkCore;
using PartBase.Application.Interfaces;
using PartBase.Infrastructure.Persistence;
using PartBase.Infrastructure.Persistence.Seed;
using PartBase.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// PostgreSql bağlantısı için gerekli olan DbContext'i ekliyoruz
builder.Services.AddDbContext<PartBaseDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// Servisleri konteynara ekliyoruz
builder.Services.AddScoped<IComponentService, ComponentService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PartBaseDbContext>();

    await db.Database.MigrateAsync();

    await SeedData.InitializeAsync(db);
}

app.Run();
