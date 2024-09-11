using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace GestaoComercial.Presentation.Extensions
{
    public static class SwaggerDocExtension
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services, IWebHostEnvironment webHost, IApiVersionDescriptionProvider provider)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, new OpenApiInfo
                        {
                            Title = $"Gestao Projeto App {description.ApiVersion}",
                            Version = description.ApiVersion.ToString(),
                            Description = "API Description. This API version has been deprecated.",
                             Contact = new OpenApiContact
                             {
                                 Name = "Rômulo Alves",
                                 Email = "romuloalves.br@gmail.com.br",
                                 Url = new Uri("https://www.linkedin.com/in/rômulo-alves-a4144113b/")
                             }
                        });

                        //Configurações para testes de autenticação
                        options.AddSecurityDefinition("Bearer",
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Entre com o TOKEN JWT",
                            Name = "Authorization",
                            Type = SecuritySchemeType.Http,
                            BearerFormat = "JWT",
                            Scheme = "Bearer"
                        });

                        //Informações adicionais para testes de autenticação
                        options.AddSecurityRequirement
                        (new OpenApiSecurityRequirement
                        {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[]{ }
                        }
                        });

                    }

                    if (!webHost.IsEnvironment("Testing"))
                    {
                        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        options.IncludeXmlComments(xmlPath);
                        options.DescribeAllParametersInCamelCase();
                        options.UseOneOfForPolymorphism();
                    }
                });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }
    }
}
