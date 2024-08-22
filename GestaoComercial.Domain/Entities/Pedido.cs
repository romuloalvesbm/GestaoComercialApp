using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Domain.Entities
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public int ClienteId { get; set; }

        public required Cliente Cliente { get; set; }
        public ICollection<PedidoProduto>? PedidoProdutos { get; set; }
    }
}
