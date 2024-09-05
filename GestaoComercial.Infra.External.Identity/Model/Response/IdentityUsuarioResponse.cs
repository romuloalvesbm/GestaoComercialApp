using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Model.Response
{
    public class IdentityUsuarioResponse
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public DateTime DataCriacao { get; set; }

        [JsonProperty("sistemaDTO")]
        public List<IdentitySistemaResponse>? IdentitySistemaResponse { get; set; }
    }
}
