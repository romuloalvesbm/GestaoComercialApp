using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Model.Response
{
    public class IdentityUsuarioPerfilSistemaResponse
    {
        public string? IdUsuarioPerfilSistema { get; set; }

        [JsonProperty("usuarioResponseDTO")]
        public IdentityUsuarioResponse? IdentityUsuarioResponse { get; set; }
    }
}
