using GestaoComercial.Infra.Redis.Extensions;
using GestaoComercial.Infra.Data.Extensions;
using GestaoComercial.Application.Extensions;
using GestaoComercial.Infra.External.Identity.Extensions;
using GestaoComercial.CrossCutting.Authorization.Extensions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using GestaoComercial.Presentation.Extensions;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerDoc(builder.Environment, provider);
builder.Services.AddJsonDoc();
builder.Services.AddCorsConfig();
builder.Services.AddJwtBearer(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddDapperConfig(builder.Configuration);
builder.Services.AddRedisConfig(builder.Configuration);
builder.Services.AddExternalIdentityConfig(builder.Configuration);
builder.Services.AddAuthorizationConfig(builder.Configuration);


var app = builder.Build();

app.UseAuthorization();
app.UseSwaggerDoc(provider);
app.UseCorsConfig();
app.MapControllers();

app.Run();
