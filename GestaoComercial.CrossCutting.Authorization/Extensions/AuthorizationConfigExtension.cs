using GestaoComercial.CrossCutting.Authorization.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.CrossCutting.Authorization.Extensions
{
    public static class AuthorizationConfigExtension
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurações de JWT
            services.Configure<JwtTokenSettingsIdentity>(configuration.GetSection("JwtTokenSettingsIdentity"));           

            services.AddTransient<JwtHelper>();

            return services;
        }
    }
}
