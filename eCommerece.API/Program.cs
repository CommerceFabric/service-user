using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerece.API.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add dependency injection services to the service collection
builder.Services.AddInfrastructureServices();
builder.Services.AddCoreServices();

// Add controllers to the service collection
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

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
