using GestaoComercial.Application.Validation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Commands
{
    public class ClienteUpdateCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Informe o id do cliente.")]
        public int ClienteId { get; set; }

        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o nome do cliente.")]
        public string? Nome { get; set; }
    }
}
