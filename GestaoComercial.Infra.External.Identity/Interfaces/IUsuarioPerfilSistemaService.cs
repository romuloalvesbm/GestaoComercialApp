using GestaoComercial.Infra.External.Identity.Model.Request;
using GestaoComercial.Infra.External.Identity.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Infra.External.Identity.Interfaces
{
    public interface IUsuarioPerfilSistemaService
    {
        Task<IdentityUsuarioPerfilSistemaResponse?> GetIdentityUsuarioPorId(Guid sistemaId, Guid userId);
        Task<CreateIdentityUsuarioPerfilSistemaResponse?> Create(CreateIdentityUsuarioPerfilSistemaRequest request);
    }
}
