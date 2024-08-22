using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Domain.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string? Nome { get; set; }

        public ICollection<Pedido>? Pedidos { get; set; }
    }
}
