using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartBase.Api.Middleware;
using PartBase.Application.Interfaces;
using PartBase.Application.Validators;
using PartBase.Infrastructure.Persistence;
using PartBase.Infrastructure.Persistence.Seed;
using PartBase.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi("v1");

// PostgreSql bağlantısı için gerekli olan DbContext'i ekliyoruz
builder.Services.AddDbContext<PartBaseDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servisleri konteynara ekliyoruz
builder.Services.AddScoped<IComponentService, ComponentService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// FluentValidation için gerekli olan validator'ları ekliyoruz

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateComponentRequestValidator>();

// Health Check ekliyoruz
builder.Services.AddHealthChecks();

// Model validation hatalarını özelleştirmek için ApiBehaviorOptions'ı yapılandırıyoruz
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value!.Errors.Count > 0)
            .Select(x => new
            {
                Field = x.Key,
                Errors = x.Value!.Errors.Select(e => e.ErrorMessage)
            });

        return new BadRequestObjectResult(new
        {
            Success = false,
            Message = "Validation failed.",
            Errors = errors
        });
    };
});

var app = builder.Build();

// Middleware'ı ekliyoruz
app.UseMiddleware<ExceptionMiddleware>();

// Health Check endpoint'ini ekliyoruz
app.MapHealthChecks("/health");

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
