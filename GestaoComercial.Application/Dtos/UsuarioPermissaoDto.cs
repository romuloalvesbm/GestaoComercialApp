using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Dtos
{
    public class UsuarioPermissaoDTO
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataHoraAcesso { get; set; }
        public List<string> Permissoes { get; set; } = [];       
    }
}
