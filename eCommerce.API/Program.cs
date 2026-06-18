using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerce.API.Middleware;
using System.Text.Json.Serialization;
using eCommerce.Core.Mappers;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text.Json;

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

// Build the web application
var app = builder.Build();

// Use custom exception handling middleware
app.UseExceptionHandlingMiddleware();

// Enable routing, authentication, authoriation, and controller mapping
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Run the web application
app.Run();
