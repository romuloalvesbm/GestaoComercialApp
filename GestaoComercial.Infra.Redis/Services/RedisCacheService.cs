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
        private readonly IDatabase _database;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _database = _connectionMultiplexer.GetDatabase();
        }       

        public async Task<T?> GetCacheAsync<T>(string key)
        {
            //var db = _connectionMultiplexer.GetDatabase();
            var serializedItem = await _database.StringGetAsync(key);

            if (serializedItem.IsNullOrEmpty)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(serializedItem!);
        }

        public IEnumerable<string> SearchKeys(string prefix)
        {
            var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First());
            var keys = server.Keys(pattern: $"{prefix}*");
            return keys.Select(k => k.ToString());
        }

        public async Task DeleteKeysByPrefixAsync(string prefix)
        {
            var keys = SearchKeys(prefix);
            foreach (var key in keys)
            {
                await _database.KeyDeleteAsync(key);
            }
        }

        public async Task SetCacheAsync<T>(string key, T item, TimeSpan expiration)
        {
            //var db = _connectionMultiplexer.GetDatabase();
            var serializedItem = JsonConvert.SerializeObject(item);

            await _database.StringSetAsync(key, serializedItem, expiration);
        }
    }
}
