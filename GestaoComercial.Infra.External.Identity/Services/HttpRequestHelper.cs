using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestaoComercial.Infra.External.Identity.Services
{
    public static class HttpRequestHelper
    {
        public static HttpRequestMessage CreateRequest(HttpMethod htttpMethod, string url, string? clientId = null, string? clientSecret = null, object? data = null)
        {
            // Cria o HttpRequestMessage com o método POST e URL fornecidos
            var request = new HttpRequestMessage(htttpMethod, url);

            if (data is not null)
            {
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                request.Content = content;
            }

            if (clientId != null)
            // Adiciona os cabeçalhos necessários
            request.Headers.Add("client_id", clientId);

            if(clientSecret != null) 
            request.Headers.Add("client_secret", clientSecret);

            return request;
        }
    }
}
