using BankingSystem.Application.Interfaces;
using BankingSystem.Infrastructure.Persistence;
using BankingSystem.Infrastructure.Persistence.Repositories;
using BankingSystem.Infrastructure.Repositories;
using BankingSystem.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BankingSystem.Infrastructure
{
    public static class InfrastructureSetup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register repositories
            services.AddScoped<AccountRepository>();
            services.AddScoped<TransactionRepository>();

            // Register Redis services
            var redisConnection = configuration["RedisSettings:ConnectionString"];
            var redisCacheDuration = int.Parse(configuration["RedisSettings:DefaultCacheDurationInMinutes"]);
            var connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnection);

            services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);
            services.AddScoped(sp => new RedisCacheService(connectionMultiplexer, redisCacheDuration));

            // Register services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<DatabaseInitializer>();
        }
    }
}
