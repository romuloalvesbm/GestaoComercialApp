using GestaoComercial.Application.Interfaces;
using GestaoComercial.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

            //configurando o AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //registrando as interfaces/classes de serviço da aplicação
            services.AddTransient<IClienteApplicationService, ClienteApplicationService>();
            services.AddTransient<IUsuarioApplicationService, UsuarioAplicationService>();

            return services;
        }
    }
}
