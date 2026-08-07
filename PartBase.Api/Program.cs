using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartBase.Api.Middleware;
using PartBase.Application.Interfaces;
using PartBase.Application.Validators;
using PartBase.Infrastructure.Identity;
using PartBase.Infrastructure.Persistence;
using PartBase.Infrastructure.Persistence.Seed;
using PartBase.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------
// Controllers
// ----------------------------------------------------

builder.Services.AddControllers();


// ----------------------------------------------------
// OpenAPI
// ----------------------------------------------------

builder.Services.AddOpenApi("v1");


// ----------------------------------------------------
// Database
// ----------------------------------------------------

builder.Services.AddDbContext<PartBaseDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// ----------------------------------------------------
// Identity
// ----------------------------------------------------
// Identity SADECE BİR KEZ kaydediliyor.

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;

        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<PartBaseDbContext>()
    .AddDefaultTokenProviders();


// ----------------------------------------------------
// Application Services
// ----------------------------------------------------

builder.Services.AddScoped<IComponentService, ComponentService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();


// ----------------------------------------------------
// FluentValidation
// ----------------------------------------------------

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();


// ----------------------------------------------------
// Health Check
// ----------------------------------------------------

builder.Services.AddHealthChecks();


// ----------------------------------------------------
// API Validation
// ----------------------------------------------------

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value!.Errors.Count > 0)
            .Select(x => new
            {
                Field = x.Key,
                Errors = x.Value!.Errors
                    .Select(e => e.ErrorMessage)
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


// ----------------------------------------------------
// Middleware
// ----------------------------------------------------

app.UseMiddleware<ExceptionMiddleware>();


// ----------------------------------------------------
// Health Check
// ----------------------------------------------------

app.MapHealthChecks("/health");


// ----------------------------------------------------
// OpenAPI
// ----------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


// ----------------------------------------------------
// HTTPS
// ----------------------------------------------------

app.UseHttpsRedirection();


// ----------------------------------------------------
// Authentication / Authorization
// ----------------------------------------------------

// JWT'yi henüz eklemedik.
// Identity'nin cookie authentication altyapısı için:

app.UseAuthentication();

app.UseAuthorization();


// ----------------------------------------------------
// Controllers
// ----------------------------------------------------

app.MapControllers();


// ----------------------------------------------------
// Database Migration + Seed
// ----------------------------------------------------

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<PartBaseDbContext>();

    await db.Database.MigrateAsync();

    await SeedData.InitializeAsync(db);
}


// ----------------------------------------------------
// Identity Role Seed
// ----------------------------------------------------

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    await IdentitySeeder.SeedRolesAsync(roleManager);
}


app.Run();