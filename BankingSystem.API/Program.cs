using BankingSystem.API.Filters;
using BankingSystem.API.Middleware;
using BankingSystem.API.Swagger;
using BankingSystem.Application.Commands.Accounts;
using BankingSystem.Application.Mappings;
using BankingSystem.Infrastructure;
using BankingSystem.Infrastructure.Configurations;
using BankingSystem.Infrastructure.Persistence;
using BankingSystem.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add AutoMapper and register profiles
builder.Services.AddAutoMapper(typeof(MappingProfile));


// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add Serilog to the logging pipeline
builder.Host.UseSerilog();
// Register MediatR service

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly); // API
    cfg.RegisterServicesFromAssembly(typeof(CreateAccountCommand).Assembly); // Application
});

// Bind configuration sections to classes
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));


// Add services to the container
builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddScoped<AuthenticationService>();



builder.Services.AddScoped<ExceptionFilter>();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Configure Authorization with Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOrManager", policy =>
        policy.RequireRole("Admin", "Manager"));
});


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
