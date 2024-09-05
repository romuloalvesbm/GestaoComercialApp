using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Model.Response
{
    public class IdentitySistemaResponse
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
        public string? Versao { get; set; }

        [JsonProperty("perfilDTOs")]
        public List<IdentityPerfilResponse>? IdentityPerfilResponse { get; set; }
    }
}

