using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Domain.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task Begin();
        Task Commit();
        Task Rollback();        

        // Aqui você pode adicionar métodos para os repositórios específicos
        IClienteRepository ClienteRepository { get; }
        IPedidoProdutoRepository PedidoProdutoRepository { get; }
        IPedidoRepository PedidoRepository { get; }
        IProdutoRepository ProdutoRepository { get; }


    }
}
