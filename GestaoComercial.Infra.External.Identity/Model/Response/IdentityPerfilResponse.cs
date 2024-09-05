using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Model.Response
{
    public class IdentityPerfilResponse
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
        public string? SistemaId { get; set; }
        public string? Sistema { get; set; }

        [JsonProperty("permissaoDTOs")]
        public List<IdentityPermissaoResponse>? IdentityPermissaoResponse { get; set; }
    }
}
