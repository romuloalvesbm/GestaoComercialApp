using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Model.Request
{
    public class CreateIdentityUsuarioPerfilSistemaRequest
    {
        [JsonProperty("perfilId")]
        public Guid PerfilId { get; set; }
        [JsonProperty("sistemaId")]
        public Guid SistemaId { get; set; }
        [JsonProperty("usuarioId")]
        public Guid UsuarioId { get; set; }
    }
}
