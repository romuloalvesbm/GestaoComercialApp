using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Settings
{
    public static class UriSetup
    {
        public static readonly string Autenticar = "api/login/autenticar";
        public static readonly string GetSistemaPorId = "api/sistema/{0}";

        public static readonly string CreateusuarioPerfilSistema = "api/usuarioperfilsistema";


        public static readonly string GetUsuarioPorId = "api/usuarioperfilsistema/obterpermissaousuario?sistemaId={0}&usuarioId={1}";
    }
}
