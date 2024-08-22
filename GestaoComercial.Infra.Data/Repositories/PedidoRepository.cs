using Dapper;
using GestaoComercial.Domain.Entities;
using GestaoComercial.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction? _transaction;

        public PedidoRepository(IDbConnection dbConnection, IDbTransaction? transaction = null)
        {
            _dbConnection = dbConnection;
            _transaction = transaction;
        }

        public async Task Inserir(Pedido obj)
        {
            var query = "Insert into PEDIDO(DATACRIACAO, CLIENTEID) values (@DataCriacao, @ClienteId)";
            await _dbConnection.ExecuteAsync(query, obj, _transaction);
        }

        public async Task Alterar(Pedido obj)
        {
            var query = "UPDATE PEDIDO SET CLIENTEID = @ClienteId WHERE PEDIDOID = @PedidoId";
            await _dbConnection.ExecuteAsync(query, obj, _transaction);
        }

        public async Task Excluir(Pedido obj)
        {
            var query = "DELETE FROM PEDIDO WHERE PEDIDOID = @PedidoId";
            await _dbConnection.ExecuteAsync(query, new { obj.ClienteId }, _transaction);
        }

        public async Task<Pedido?> ObterPorId(int id)
        {
            var query = @"
                        SELECT
                            p.PedidoId,
                            p.DataCriacao,
                            p.ClienteId,
                            c.ClienteId AS Cliente_ClienteId,
                            c.Nome AS Cliente_Nome,
                            pp.PedidoProdutoId,
                            pp.ProdutoId,
                            pp.Quantidade,
                            pr.ProdutoId AS Produto_ProdutoId,
                            pr.Nome AS Produto_Nome
                        FROM Pedido p
                        LEFT JOIN Cliente c ON p.ClienteId = c.ClienteId
                        LEFT JOIN PedidoProduto pp ON p.PedidoId = pp.PedidoId
                        LEFT JOIN Produto pr ON pp.ProdutoId = pr.ProdutoId
                        WHERE p.PedidoId = @PedidoId";

            var pedidoDictionary = new Dictionary<int, Pedido>();

            var result = await _dbConnection.QueryAsync<Pedido, Cliente, PedidoProduto, Produto, Pedido>(
                query,
                (pedido, cliente, pedidoProduto, produto) =>
                {
                    if (!pedidoDictionary.TryGetValue(pedido.PedidoId, out var pedidoEntry))
                    {
                        pedidoEntry = pedido;
                        pedidoEntry.PedidoProdutos = [];
                        pedidoEntry.Cliente = cliente;
                        pedidoDictionary.Add(pedido.PedidoId, pedidoEntry);
                    }

                    if (pedidoProduto != null)
                    {
                        // Configurando PedidoProduto e associando Produto
                        pedidoProduto.Pedido = pedidoEntry;
                        if (produto != null)
                        {
                            pedidoProduto.Produto = produto;
                        }
                        pedidoEntry.PedidoProdutos!.Add(pedidoProduto);
                    }

                    return pedidoEntry;
                },
                new { PedidoId = id },
                splitOn: "Cliente_ClienteId,PedidoProdutoId,Produto_ProdutoId",  // Indica onde cada objeto começa no resultado
                transaction: _transaction
            );

            return pedidoDictionary.Values.FirstOrDefault();
        }

        public async Task<ICollection<Pedido>> Consultar(string? nome)
        {
            var query = @"
                        SELECT
                            p.PedidoId,
                            p.DataCriacao,
                            p.ClienteId,
                            c.ClienteId AS Cliente_ClienteId,
                            c.Nome AS Cliente_Nome,
                            pp.PedidoProdutoId,
                            pp.ProdutoId,
                            pp.Quantidade,
                            pr.ProdutoId AS Produto_ProdutoId,
                            pr.Nome AS Produto_Nome
                        FROM Pedido p
                        LEFT JOIN Cliente c ON p.ClienteId = c.ClienteId
                        LEFT JOIN PedidoProduto pp ON p.PedidoId = pp.PedidoId
                        LEFT JOIN Produto pr ON pp.ProdutoId = pr.ProdutoId
                        WHERE p.PedidoId = @PedidoId";

            var pedidoDictionary = new Dictionary<int, Pedido>();

            var result = await _dbConnection.QueryAsync<Pedido, Cliente, PedidoProduto, Produto, Pedido>(
                query,
                (pedido, cliente, pedidoProduto, produto) =>
                {
                    if (!pedidoDictionary.TryGetValue(pedido.PedidoId, out var pedidoEntry))
                    {
                        pedidoEntry = pedido;
                        pedidoEntry.PedidoProdutos = [];
                        pedidoEntry.Cliente = cliente;
                        pedidoDictionary.Add(pedido.PedidoId, pedidoEntry);
                    }

                    if (pedidoProduto != null)
                    {
                        // Configurando PedidoProduto e associando Produto
                        pedidoProduto.Pedido = pedidoEntry;
                        pedidoProduto.Produto = produto;
                        pedidoEntry.PedidoProdutos!.Add(pedidoProduto);
                    }

                    return pedidoEntry;
                },               
                splitOn: "Cliente_ClienteId,PedidoProdutoId,Produto_ProdutoId",  // Indica onde cada objeto começa no resultado
                transaction: _transaction
            );

            return pedidoDictionary.Values.ToList();
        }       

        public async ValueTask DisposeAsync()
        {
            _transaction?.Dispose();

            // Fechando e descartando a conexão com o banco de dados
            _dbConnection.Dispose();

            GC.SuppressFinalize(this);

            await Task.CompletedTask;
        }
    }
}
