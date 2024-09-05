using GestaoComercial.Application.Models;
using GestaoComercial.Application.Validation;
using GestaoComercial.Infra.External.Identity.Model.Request;
using GestaoComercial.Infra.External.Identity.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Interfaces
{
    public interface IUsuarioApplicationService
    {
        Task<Result<AutenticarUsuarioResponseModel>> GetToken(AutenticarUsuarioRequestModel model);
        Task<Result<IdentityUsuarioPerfilSistemaResponse?>> GetIdentityUsuarioPorId(Guid sistemaId, Guid userId);

    }
}

