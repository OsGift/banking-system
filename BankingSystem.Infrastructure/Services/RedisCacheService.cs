using StackExchange.Redis;
using System.Text.Json;

namespace BankingSystem.Infrastructure.Services
{
    public class RedisCacheService
    {
        private readonly IDatabase _database;
        private readonly TimeSpan _defaultCacheDuration;

        public RedisCacheService(IConnectionMultiplexer redis, int defaultCacheDurationInMinutes)
        {
            _database = redis.GetDatabase();
            _defaultCacheDuration = TimeSpan.FromMinutes(defaultCacheDurationInMinutes);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var cachedData = await _database.StringGetAsync(key);
            if (string.IsNullOrEmpty(cachedData)) return default;

            return JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var data = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, data, expiration ?? _defaultCacheDuration);
        }

        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}
