using GestaoComercial.Infra.Redis.Interfaces;
using GestaoComercial.Infra.Redis.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.Redis.Extensions
{
    public static class RedisConfigExtension
    {
        public static IServiceCollection AddRedisConfig(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configurationOptions = ConfigurationOptions.Parse(configuration.GetSection("RedisSettings:ConnectionString").Value!, true);
                return ConnectionMultiplexer.Connect(configurationOptions);
            });

            services.AddSingleton<IRedisCacheService, RedisCacheService>();

            return services;
        }
    }
}
