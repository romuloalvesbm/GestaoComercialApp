﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Domain.Entities
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string? Nome { get; set; }
        public decimal Preco { get; set; }

        public ICollection<PedidoProduto>? PedidoProdutos { get; set; }
    }
}
