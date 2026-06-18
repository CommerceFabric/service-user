using eCommerce.API.Middleware;
using eCommerce.Core;
using eCommerce.Core.Mappers;
using eCommerce.Infrastructure;
using FluentValidation.AspNetCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add dependency injection services to the service collection
builder.Services.AddInfrastructureServices();
builder.Services.AddCoreServices();

// Add controllers to the service collection
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true));
    });

// Add automapper to the service collection
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(ApplicationUserMappingProfile));

// Add Fluentvalidations to use as contract validators for the DTOs
builder.Services.AddFluentValidationAutoValidation();

// Add swagger generation to create API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Cors Services
builder.Services.AddCors(options =>
{
    // Configure a default CORS policy that allows any origin, method, and header
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Build the web application
var app = builder.Build();

// Use custom exception handling middleware
app.UseExceptionHandlingMiddleware();

// Enable routing
app.UseRouting();

// Enable swagger UI and endpoint for API documentation
app.UseSwagger(); // adds endpoint that can serve the swagger.json
app.UseSwaggerUI(); // adds swagger UI to visualize and interact with the API's resources
app.UseCors(); // Enable Cross-Origin Resource Sharing (CORS) using the configured policy, controlling which origins, methods, and headers can access this API.

// Enable authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Maps controller endpoints to the application
app.MapControllers(); 

// Run the web application
app.Run();
