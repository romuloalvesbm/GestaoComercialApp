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
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction? _transaction;

        public ClienteRepository(IDbConnection dbConnection, IDbTransaction? transaction = null)
        {
            _dbConnection = dbConnection;
            _transaction = transaction;
        }

        public async Task Inserir(Cliente obj)
        {
            var query = "Insert into CLIENTE(NOME) values (@Nome)";
            await _dbConnection.ExecuteAsync(query, obj, _transaction);
        }

        public async Task Alterar(Cliente obj)
        {
            var query = "UPDATE CLIENTE SET NOME = @Nome WHERE CLIENTEID = @ClienteId";
            await _dbConnection.ExecuteAsync(query, obj, _transaction);
        }

        public async Task Excluir(Cliente obj)
        {
            var query = "DELETE FROM CLIENTE WHERE CLIENTEID = @ClienteId";
            await _dbConnection.ExecuteAsync(query, new { obj.ClienteId }, _transaction);
        }

        public async Task<ICollection<Cliente>> Consultar(string? nome = null)
        {
            string query = @"
                SELECT c.*, p.*
                FROM CLIENTE c
                LEFT JOIN PEDIDO p ON c.CLIENTEID = p.CLIENTEID
                WHERE @Nome IS NULL OR c.NOME LIKE @Nome";

            var clienteDictionary = new Dictionary<int, Cliente>();

            var queryResult = await _dbConnection.QueryAsync<Cliente, Pedido, Cliente>(
                    query,
                    (cliente, pedido) =>
                    {
                        if (!clienteDictionary.TryGetValue(cliente.ClienteId, out var clienteEntry))
                        {
                            clienteEntry = cliente;
                            clienteEntry.Pedidos = [];
                            clienteDictionary.Add(cliente.ClienteId, clienteEntry);
                        }

                        if (pedido != null)
                        {
                            clienteEntry.Pedidos!.Add(pedido);
                        }

                        return clienteEntry;
                    },
                     new { Nome = nome != null ? $"{nome}%" : null },
                    splitOn: "PedidoId",  
                    transaction: _transaction
                );

            return queryResult.ToList();
        }

        public async Task<Cliente?> ObterPorId(int id)
        {
            var query = "SELECT * FROM CLIENTE WHERE CLIENTEID = @CLIENTEID";
            return await _dbConnection.QueryFirstOrDefaultAsync<Cliente>(query, new { CLIENTEID = id }, _transaction);
        }        

        public async Task<Cliente?> ObterPorNome(string? nome)
        {
            var query = "SELECT * FROM CLIENTE WHERE NOME = @nome";
            return await _dbConnection.QueryFirstOrDefaultAsync<Cliente>(query, new { nome }, _transaction);
        }

        public async ValueTask DisposeAsync()
        {
            _transaction?.Dispose();

            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close(); // Fecha a conexão se estiver aberta
            }

            // Fechando e descartando a conexão com o banco de dados
            _dbConnection.Dispose();

            GC.SuppressFinalize(this);

            await Task.CompletedTask;
        }
    }
}
