using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Model.Response
{
    public class CreateIdentityUsuarioPerfilSistemaResponse
    {
        public Guid IdUsuarioPerfilSistema { get; set; }
        public Guid UsuarioId { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public Guid SistemaId { get; set; }
        public string Sistema { get; set; } = string.Empty;
        public Guid PerfilId { get; set; }
        public string Perfil { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
    }
}
