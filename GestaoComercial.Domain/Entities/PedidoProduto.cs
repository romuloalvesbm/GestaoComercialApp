using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Domain.Entities
{
    public class PedidoProduto
    {
        public int PedidoProdutoId { get; set; }
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }

        public required Pedido Pedido { get; set; }
        public required Produto Produto { get; set; }
    }
}
