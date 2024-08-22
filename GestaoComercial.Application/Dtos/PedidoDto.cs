using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Dtos
{
    public class PedidoDto
    {
        public int PedidoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public int ClienteId { get; set; }
    }
}
