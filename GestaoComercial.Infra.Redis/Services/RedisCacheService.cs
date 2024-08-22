using GestaoComercial.Infra.Redis.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.Redis.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<T?> GetCacheAsync<T>(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var serializedItem = await db.StringGetAsync(key);

            if (serializedItem.IsNullOrEmpty)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(serializedItem!);
        }

        public async Task SetCacheAsync<T>(string key, T item, TimeSpan expiration)
        {
            var db = _connectionMultiplexer.GetDatabase();
            var serializedItem = JsonConvert.SerializeObject(item);

            await db.StringSetAsync(key, serializedItem, expiration);
        }
    }
}
