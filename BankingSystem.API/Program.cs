using BankingSystem.API.Filters;
using BankingSystem.API.Middleware;
using BankingSystem.API.Swagger;
using BankingSystem.Infrastructure;
using BankingSystem.Infrastructure.Persistence;
using BankingSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddScoped<AuthenticationService>(sp =>
    new AuthenticationService(builder.Configuration["JwtSecret"]));
builder.Services.AddScoped<ExceptionFilter>();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


// Configure Infrastructure
InfrastructureSetup.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();



// Middleware pipeline
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<HealthCheckMiddleware>();
app.UseMiddleware<RBACMiddleware>();

app.UseSwaggerDocumentation();

app.UseRouting();
app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var dbInitializer = services.GetRequiredService<DatabaseInitializer>();
await dbInitializer.SeedAsync();

app.Run();
