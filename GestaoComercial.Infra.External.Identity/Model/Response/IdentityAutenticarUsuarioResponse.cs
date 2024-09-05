using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Model.Response
{
    public class IdentityAutenticarUsuarioResponse
    {
        public string? usuarioId { get; set; }
        public string? nome { get; set; }
        public string? email { get; set; }
        public DateTime? dataHoraAcesso { get; set; }
        public string? accessToken { get; set; }
    }
}
