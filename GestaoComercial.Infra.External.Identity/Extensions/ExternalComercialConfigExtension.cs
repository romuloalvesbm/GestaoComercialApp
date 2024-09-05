using GestaoComercial.Infra.External.Identity.Interfaces;
using GestaoComercial.Infra.External.Identity.Services;
using GestaoComercial.Infra.External.Identity.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Extensions
{
    public static class ExternalComercialConfigExtension
    {
        public static IServiceCollection ExternalComercialConfig(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurações de ApiSettings
            services.Configure<ApiSettingsIdentity>(configuration.GetSection("ApiSettingsIdentity"));

            // Configurações de JWT
            services.Configure<JwtTokenSettings>(configuration.GetSection("JwtTokenSettings"));

            // Configuração do HttpClientFactory
            services.AddHttpClient("ApiExternalIdentity", client =>
            {
                client.BaseAddress = new Uri(configuration["ApiSettingsIdentity:BaseAddress"]!);
                client.DefaultRequestHeaders.Add("X-ApiVersion", configuration["ApiSettingsIdentity:XApiVersion"]);
                // Outros cabeçalhos padrão, se necessário
            });

            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IUsuarioPerfilSistemaService, UsuarioPerfilSistemaService>();

            return services;
        }
    }
}
