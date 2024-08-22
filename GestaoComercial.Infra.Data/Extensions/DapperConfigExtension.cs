using GestaoComercial.Domain.Interfaces;
using GestaoComercial.Infra.Data.Repositories;
using GestaoComercial.Infra.Data.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.Data.Extensions
{
    public static class DapperConfigExtension
    {
        public static IServiceCollection AddDapperConfig(this IServiceCollection services, IConfiguration configuration) 
        {
            // Configura DapperSettings
            services.Configure<DapperSettings>(configuration.GetSection("DapperSettings"));

            // Registra IDbConnection
            services.AddTransient<IDbConnection>(sp =>
            {
                var dapperSettings = sp.GetRequiredService<IOptions<DapperSettings>>().Value;
                return new SqlConnection(dapperSettings.ConnectionString);
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IPedidoProdutoRepository, PedidoProdutoRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}
