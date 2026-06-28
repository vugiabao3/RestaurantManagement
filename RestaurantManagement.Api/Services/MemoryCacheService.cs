using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.API.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<string, byte> _keys = new();

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan expiration)
        {
            if (_cache.TryGetValue<T>(key, out var value)) return value!;
            var created = await factory();
            _cache.Set(key, created, expiration);
            _keys.TryAdd(key, 0);
            return created;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
            _keys.TryRemove(key, out _);
        }

        public void RemoveByPrefix(string prefix)
        {
            foreach (var key in _keys.Keys.Where(x => x.StartsWith(prefix, StringComparison.Ordinal)))
                Remove(key);
        }
    }
}
