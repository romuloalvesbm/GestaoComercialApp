using GestaoComercial.Application.Commands;
using GestaoComercial.Application.Dtos;
using GestaoComercial.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Interfaces
{
    public interface IClienteApplicationService
    {
        Task<Result> Inserir(ClienteCreateCommand command);
        Task<Result> Alterar(ClienteUpdateCommand command);
        Task<Result> Excluir(ClienteDeleteCommand command);
        Task<Result<ICollection<ClienteDto>>> Consulta(string? nome);
    }
}
