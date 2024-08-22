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
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction? _transaction;

        public ProdutoRepository(IDbConnection dbConnection, IDbTransaction? transaction = null)
        {
            _dbConnection = dbConnection;
            _transaction = transaction;
        }

        public async Task Inserir(Produto obj)
        {
            var query = "Insert into PRODUTO(NOME, PRECO) values (@Nome, @Preco)";
            await _dbConnection.ExecuteAsync(query, obj, _transaction);
        }

        public async Task Alterar(Produto obj)
        {
            var query = "UPDATE PRODUTO SET NOME = @Nome, PRECO = @Preco WHERE PRODUTOID = @ProdutoId";
            await _dbConnection.ExecuteAsync(query, obj, _transaction);
        }

        public async Task Excluir(Produto obj)
        {
            var query = "DELETE FROM PRODUTO WHERE PRODUTOID = @ProdutoId";
            await _dbConnection.ExecuteAsync(query, new { obj.ProdutoId }, _transaction);
        }

        public async Task<ICollection<Produto>> Consultar(string? nome)
        {
            var query = @"
                        SELECT
                            p.ProdutoId,
                            p.Nome,
                            p.Preco,
                            pp.PedidoProdutoId,
                            pp.PedidoId,
                            pp.Quantidade,
                            pe.PedidoId AS Pedido_PedidoId,
                            pe.DataCriacao AS Pedido_DataCriacao,
                            pe.ClienteId AS Pedido_ClienteId
                        FROM Produto p
                        LEFT JOIN PedidoProduto pp ON p.ProdutoId = pp.ProdutoId
                        LEFT JOIN Pedido pe ON pp.PedidoId = pe.PedidoId
                        WHERE p.ProdutoId = @ProdutoId";

            var produtoDictionary = new Dictionary<int, Produto>();

            var result = await _dbConnection.QueryAsync<Produto, PedidoProduto, Pedido, Produto>(
                query,
                (produto, pedidoProduto, pedido) =>
                {
                    if (!produtoDictionary.TryGetValue(produto.ProdutoId, out var produtoEntry))
                    {
                        produtoEntry = produto;
                        produtoEntry.PedidoProdutos = [];
                        produtoDictionary.Add(produto.ProdutoId, produtoEntry);
                    }

                    if (pedidoProduto != null)
                    {
                        pedidoProduto.Produto = produtoEntry;
                        if (pedido != null)
                        {
                            pedidoProduto.Pedido = pedido;
                        }
                        produtoEntry.PedidoProdutos!.Add(pedidoProduto);
                    }

                    return produtoEntry;
                },               
                splitOn: "PedidoProdutoId,Pedido_PedidoId",
                transaction: _transaction
            );

            return produtoDictionary.Values.ToList();
        }          

        public async Task<Produto?> ObterPorId(int id)
        {
            var query = @"
                        SELECT
                            p.ProdutoId,
                            p.Nome,
                            p.Preco,
                            pp.PedidoProdutoId,
                            pp.PedidoId,
                            pp.Quantidade,
                            pe.PedidoId AS Pedido_PedidoId,
                            pe.DataCriacao AS Pedido_DataCriacao,
                            pe.ClienteId AS Pedido_ClienteId
                        FROM Produto p
                        LEFT JOIN PedidoProduto pp ON p.ProdutoId = pp.ProdutoId
                        LEFT JOIN Pedido pe ON pp.PedidoId = pe.PedidoId
                        WHERE p.ProdutoId = @ProdutoId";

            var produtoDictionary = new Dictionary<int, Produto>();

            var result = await _dbConnection.QueryAsync<Produto, PedidoProduto, Pedido, Produto>(
                query,
                (produto, pedidoProduto, pedido) =>
                {
                    if (!produtoDictionary.TryGetValue(produto.ProdutoId, out var produtoEntry))
                    {
                        produtoEntry = produto;
                        produtoEntry.PedidoProdutos = [];
                        produtoDictionary.Add(produto.ProdutoId, produtoEntry);
                    }

                    if (pedidoProduto != null)
                    {
                        pedidoProduto.Produto = produtoEntry;
                        if (pedido != null)
                        {
                            pedidoProduto.Pedido = pedido;
                        }
                        produtoEntry.PedidoProdutos!.Add(pedidoProduto);
                    }

                    return produtoEntry;
                },
                new { ProdutoId = id },
                splitOn: "PedidoProdutoId,Pedido_PedidoId",
                transaction: _transaction
            );

            return produtoDictionary.Values.FirstOrDefault();
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
