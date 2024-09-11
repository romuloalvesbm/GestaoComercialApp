using GestaoComercial.Infra.External.Identity.Interfaces;
using GestaoComercial.Infra.External.Identity.Model.Request;
using GestaoComercial.Infra.External.Identity.Model.Response;
using GestaoComercial.Infra.External.Identity.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Services
{
    public class UsuarioPerfilSistemaService : IUsuarioPerfilSistemaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;

        public UsuarioPerfilSistemaService(IHttpClientFactory httpClientFactory, ILoginService loginService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }

        public async Task<CreateIdentityUsuarioPerfilSistemaResponse?> Create(CreateIdentityUsuarioPerfilSistemaRequest request)
        {
            var token = await _loginService.GetToken(null);

            var request1 = HttpRequestHelper.CreateRequest(HttpMethod.Post, UriSetup.CreateusuarioPerfilSistema, null, null, request);
            //Console.WriteLine(await request1.Content.ReadAsStringAsync());

            // Cria uma instância de HttpClient usando o IHttpClientFactory
            request1.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token!.AccessToken);

            var client = _httpClientFactory.CreateClient("ApiExternalIdentity");

            // Envia a requisição
            var response = await client.SendAsync(request1);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Deserialize the content to your model
                return JsonConvert.DeserializeObject<CreateIdentityUsuarioPerfilSistemaResponse>(content);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erro: {response.StatusCode}, Conteúdo: {errorContent}");               
            }

            return null;
        }

        public async Task<IdentityUsuarioPerfilSistemaResponse?> GetIdentityUsuarioPorId(Guid sistemaId, Guid userId)
        {
            var token = await _loginService.GetToken(null);

            var request = HttpRequestHelper.CreateRequest(HttpMethod.Get, string.Format(UriSetup.GetUsuarioPorId, sistemaId, userId));

            // Cria uma instância de HttpClient usando o IHttpClientFactory
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token!.AccessToken);

            var client = _httpClientFactory.CreateClient("ApiExternalIdentity");

            // Envia a requisição
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Deserialize the content to your model
                return JsonConvert.DeserializeObject<IdentityUsuarioPerfilSistemaResponse>(content);
            }

            return null;
        }
    }
}
