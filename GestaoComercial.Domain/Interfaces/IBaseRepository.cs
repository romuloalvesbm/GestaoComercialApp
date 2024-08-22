using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Domain.Interfaces
{
    public interface IBaseRepository<T> : IAsyncDisposable
    where T : class
    {
        Task Inserir(T obj);
        Task Alterar(T obj);
        Task Excluir(T obj);
        Task<ICollection<T>> Consultar(string? nome);
        Task<T?> ObterPorId(int id);
    }
}
