using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Settings
{
    public class ClienteSettings
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
    }

    public class ApiSettingsIdentity
    {
        public string? XApiVersion { get; set; }
        public ClienteSettings? Client { get; set; }
        public string? BaseAddress { get; set; }
        public required string User { get; set; }
        public required string Password { get; set; }
        public required string SistemaId { get; set; }
    }
}
