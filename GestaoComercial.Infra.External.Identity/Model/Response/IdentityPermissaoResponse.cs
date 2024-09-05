using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Model.Response
{
    public class IdentityPermissaoResponse
    {
        public string? Id { get; set; }
        public string? Descricao { get; set; }
        public string? ChaveAutorizacao { get; set; }
    }
}
