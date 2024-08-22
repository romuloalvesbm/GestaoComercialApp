using GestaoComercial.Domain.Interfaces;
using GestaoComercial.Infra.Data.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection connection;
        private IDbTransaction? transaction;

        public UnitOfWork(IOptions<DapperSettings> dapperSettings)
        {
            connection = new SqlConnection(dapperSettings.Value.ConnectionString);          
        }

        public IClienteRepository ClienteRepository => new ClienteRepository(connection, transaction);
        public IPedidoProdutoRepository PedidoProdutoRepository => new PedidoProdutoRepository(connection, transaction);
        public IPedidoRepository PedidoRepository => new PedidoRepository(connection, transaction);
        public IProdutoRepository ProdutoRepository => new ProdutoRepository(connection, transaction);

        public Task Begin()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open(); // Abre a conexão se ainda não estiver aberta
            }

            transaction = connection.BeginTransaction();
            return Task.CompletedTask;
        }       

        public async Task Commit()
        {
            if (transaction != null)
            {
                transaction.Commit(); 
                await DisposeAsync();
            }
        } 
        
        public async Task Rollback()
        {
            if (transaction != null)
            {
                transaction.Rollback(); // Método síncrono
                await DisposeAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }

            if (connection.State == ConnectionState.Open)
            {
                connection.Close(); // Fecha a conexão se estiver aberta
            }

            connection.Dispose();
            GC.SuppressFinalize(this);

            await Task.CompletedTask;
        }
    }
}
