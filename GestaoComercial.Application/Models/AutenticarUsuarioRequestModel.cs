using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Models
{
    public class AutenticarUsuarioRequestModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email de acesso.")]
        public string Email { get; set; } = string.Empty;

        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a senha de acesso.")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o id do sistema.")]
        public Guid SistemaId { get; set; }

    }
}
