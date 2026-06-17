using eCommerce.Infrastructure;
using eCommerce.Core;

var builder = WebApplication.CreateBuilder(args);

// Add dependency injection services to the service collection
builder.Services.AddInfrastructureServices();
builder.Services.AddCoreServices();

// Add controllers to the service collection
builder.Services.AddControllers();

// Build the web application
var app = builder.Build();

// Enable routing, authentication, authoriation, and controller mapping
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Run the web application
app.Run();
