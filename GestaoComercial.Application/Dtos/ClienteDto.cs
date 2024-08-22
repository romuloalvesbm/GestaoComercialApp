using GestaoComercial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Dtos
{
    public class ClienteDto
    {
        public int ClienteId { get; set; }
        public string? Nome { get; set; }

        public ICollection<PedidoDto>? PedidoDtos { get; set; }
    }
}
