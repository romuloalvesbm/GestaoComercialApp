using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.Redis.Interfaces
{
    public interface IRedisCacheService
    {
        Task SetCacheAsync<T>(string key, T item, TimeSpan expiration);
        Task<T?> GetCacheAsync<T>(string key);
    }
}
