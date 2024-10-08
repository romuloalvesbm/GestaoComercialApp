﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Models
{
    public class AutenticarUsuarioResponseModel
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataHoraAcesso { get; set; }        
        public string AccessToken { get; set; } = string.Empty;
    }
}
