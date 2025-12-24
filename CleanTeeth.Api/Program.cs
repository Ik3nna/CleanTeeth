using MediatR;
using CleanTeeth.Application.Common.Behaviours;
using CleanTeeth.Application.DentalOffices.Commands.CreateDentalOffice;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Register Validators
builder.Services.AddValidatorsFromAssembly(
    typeof(CreateDentalOfficeCommand).Assembly
);

// Register MediatR
builder.Services.AddMediatR(typeof(CreateDentalOfficeCommand).Assembly);

// =====================================================================
    // PIPELINE BEHAVIORS (MEDIATR)
// =====================================================================
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));

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

app.UseHttpsRedirection();
app.Run();
