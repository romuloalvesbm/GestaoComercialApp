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
    public class PedidoProdutoRepository : IPedidoProdutoRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction? _transaction;

        public PedidoProdutoRepository(IDbConnection dbConnection, IDbTransaction? transaction = null)
        {
            _dbConnection = dbConnection;
            _transaction = transaction;
        }

        public Task Alterar(PedidoProduto obj)
        {
            throw new NotImplementedException();
        }     

        public Task Excluir(PedidoProduto obj)
        {
            throw new NotImplementedException();
        }

        public Task Inserir(PedidoProduto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<PedidoProduto?> ObterPorId(int id)
        {
            var query = @"
                        SELECT
                            pp.PedidoProdutoId,
                            pp.PedidoId,
                            pp.ProdutoId,
                            pp.Quantidade,
                            p.PedidoId AS Pedido_PedidoId,
                            p.DataCriacao AS Pedido_DataCriacao,
                            p.ClienteId AS Pedido_ClienteId,
                            pr.ProdutoId AS Produto_ProdutoId,
                            pr.Nome AS Produto_Nome,
                            pr.Preco AS Produto_Preco
                        FROM PedidoProduto pp
                        LEFT JOIN Pedido p ON pp.PedidoId = p.PedidoId
                        LEFT JOIN Produto pr ON pp.ProdutoId = pr.ProdutoId
                        WHERE pp.PedidoProdutoId = @PedidoProdutoId";

            var pedidoProdutoDictionary = new Dictionary<int, PedidoProduto>();

            var result = await _dbConnection.QueryAsync<PedidoProduto, Pedido, Produto, PedidoProduto>(
                query,
                (pedidoProduto, pedido, produto) =>
                {
                    if (!pedidoProdutoDictionary.TryGetValue(pedidoProduto.PedidoProdutoId, out var pedidoProdutoEntry))
                    {
                        pedidoProdutoEntry = pedidoProduto;
                        pedidoProdutoEntry.Pedido = pedido;
                        pedidoProdutoEntry.Produto = produto;
                        pedidoProdutoDictionary.Add(pedidoProduto.PedidoProdutoId, pedidoProdutoEntry);
                    }
                    else
                    {
                        // Associar Pedido e Produto ao PedidoProduto existente
                        pedidoProdutoEntry.Pedido = pedido;
                        pedidoProdutoEntry.Produto = produto;
                    }

                    return pedidoProdutoEntry;
                },
                new { PedidoProdutoId = id },
                splitOn: "Pedido_PedidoId,Produto_ProdutoId",
                transaction: _transaction
            );

            return pedidoProdutoDictionary.Values.FirstOrDefault();
        }

        public async Task<ICollection<PedidoProduto>> Consultar(string? nome)
        {
            var query = @"
                        SELECT
                            pp.PedidoProdutoId,
                            pp.PedidoId,
                            pp.ProdutoId,
                            pp.Quantidade,
                            p.PedidoId AS Pedido_PedidoId,
                            p.DataCriacao AS Pedido_DataCriacao,
                            p.ClienteId AS Pedido_ClienteId,
                            pr.ProdutoId AS Produto_ProdutoId,
                            pr.Nome AS Produto_Nome,
                            pr.Preco AS Produto_Preco
                        FROM PedidoProduto pp
                        LEFT JOIN Pedido p ON pp.PedidoId = p.PedidoId
                        LEFT JOIN Produto pr ON pp.ProdutoId = pr.ProdutoId
                        WHERE pp.PedidoProdutoId = @PedidoProdutoId";

            var pedidoProdutoDictionary = new Dictionary<int, PedidoProduto>();

            var result = await _dbConnection.QueryAsync<PedidoProduto, Pedido, Produto, PedidoProduto>(
                query,
                (pedidoProduto, pedido, produto) =>
                {
                    if (!pedidoProdutoDictionary.TryGetValue(pedidoProduto.PedidoProdutoId, out var pedidoProdutoEntry))
                    {
                        pedidoProdutoEntry = pedidoProduto;
                        pedidoProdutoEntry.Pedido = pedido;
                        pedidoProdutoEntry.Produto = produto;
                        pedidoProdutoDictionary.Add(pedidoProduto.PedidoProdutoId, pedidoProdutoEntry);
                    }
                    else
                    {
                        // Associar Pedido e Produto ao PedidoProduto existente
                        pedidoProdutoEntry.Pedido = pedido;
                        pedidoProdutoEntry.Produto = produto;
                    }

                    return pedidoProdutoEntry;
                },
                splitOn: "Pedido_PedidoId,Produto_ProdutoId",
                transaction: _transaction
            );

            return pedidoProdutoDictionary.Values.ToList();
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
