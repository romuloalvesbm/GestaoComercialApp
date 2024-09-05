using GestaoComercial.Infra.External.Identity.Interfaces;
using GestaoComercial.Infra.External.Identity.Model.Request;
using GestaoComercial.Infra.External.Identity.Model.Response;
using GestaoComercial.Infra.External.Identity.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Services
{
    public class LoginService : ILoginService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiSettingsIdentity _apiSettings;

        public LoginService(IHttpClientFactory httpClientFactory, IOptions<ApiSettingsIdentity> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IdentityAutenticarUsuarioResponse?> GetToken(LoginIdentityRequest? model)
        {
            // Cria uma instância de HttpClient usando o IHttpClientFactory
            var client = _httpClientFactory.CreateClient("ApiExternalIdentity");

            var clientId = _apiSettings.Client!.ClientId;
            var clientSecret = _apiSettings.Client!.ClientSecret;

            //var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "api/login/autenticar")
            //{
            //    Content = new StringContent(JsonConvert.SerializeObject(new
            //    {
            //        email = "adminSystem33@gmail.com",
            //        senha = "Admin#3000",
            //        sistemaId = "6a88cdcc-d445-44d1-8ca3-7dd60ddc1af3"
            //    }), Encoding.UTF8, "application/json")
            //};

            //// Adiciona os cabeçalhos necessários
            //tokenRequest.Headers.Add("client_id", clientId);
            //tokenRequest.Headers.Add("client_secret", clientSecret);

            //var parameters = new Dictionary<string, string>
            //{
            //    { "email", _apiSettings.User },
            //    { "senha", _apiSettings.Password },
            //    { "sistemaId", _apiSettings.SistemaId }
            //};

            model ??= new LoginIdentityRequest
                {
                    Email = _apiSettings.User,
                    Senha = _apiSettings.Password,
                    SistemaId = _apiSettings.SistemaId
                };

            var tokenRequest = HttpRequestHelper.CreateRequest(HttpMethod.Post, UriSetup.Autenticar, clientId, clientSecret, model);

            var response = await client.SendAsync(tokenRequest);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IdentityAutenticarUsuarioResponse>(content);              
            }

            return null;
        }
    }
}
