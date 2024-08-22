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
    public class ClienteDeleteCommand : IRequest<Result>
    {
        [Required(ErrorMessage = "Informe o id do cliente.")]
        public int ClienteId { get; set; }
    }
}
