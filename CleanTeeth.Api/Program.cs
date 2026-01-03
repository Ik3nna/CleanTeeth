using MediatR;
using CleanTeeth.Application.Common.Behaviours;
using CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;
using FluentValidation;
using CleanTeeth.Application.Common.Mappings;
using Microsoft.EntityFrameworkCore;
using CleanTeeth.Infrastructure.Data;
using CleanTeeth.Domain.Interfaces;
using CleanTeeth.Infrastructure.Repositories;
using CleanTeeth.API.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// =====================================================================
    // DB Context
// =====================================================================
builder.Services.AddDbContext<CleanTeethDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CleanTeethConnectionString"))
);

// =====================================================================
    // VALIDATION, MAPPING
// =====================================================================
builder.Services.AddValidatorsFromAssembly(
    typeof(CreateDentalOfficeCommand).Assembly
);
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfiles>();
});

// Register MediatR
builder.Services.AddMediatR(typeof(CreateDentalOfficeCommand).Assembly);

// =====================================================================
    // PIPELINE BEHAVIORS (MEDIATR)
// =====================================================================
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));

// =====================================================================
    // CORE & INFRASTRUCTURE
// =====================================================================
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IDentalOfficeRepository, DentalOfficeRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDentistRepository, DentistRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add API Explorer services (required for Swagger)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CleanTeeth API",
        Version = "v1",
        Description = "API for CleanTeeth application"
    });

    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Enable Swagger middleware in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanTeeth API v1");
        c.RoutePrefix = string.Empty; // Makes Swagger UI available at the app's root (https://localhost:xxxx/)
    });
}

app.UseCustomExceptionHandler();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
