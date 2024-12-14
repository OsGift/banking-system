using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Infrastructure.Services
{
    public class CachingService
    {
        private readonly IMemoryCache _cache;

        public CachingService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Set(string key, object value, TimeSpan duration)
        {
            _cache.Set(key, value, duration);
        }

        public object? Get(string key)
        {
            _cache.TryGetValue(key, out var value);
            return value;
        }
    }
}
