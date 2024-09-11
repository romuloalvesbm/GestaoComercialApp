using AutoMapper;
using GestaoComercial.Application.Commands;
using GestaoComercial.Application.Dtos;
using GestaoComercial.Application.Models;
using GestaoComercial.Domain.Entities;
using GestaoComercial.Infra.External.Identity.Model.Request;
using GestaoComercial.Infra.External.Identity.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Cliente

            CreateMap<ClienteCreateCommand, Cliente>();

            CreateMap<ClienteUpdateCommand, Cliente>();

            CreateMap<ClienteDeleteCommand, Cliente>();

            #endregion

            #region Usuario

            CreateMap<AutenticarUsuarioRequestModel, LoginIdentityRequest>();
            CreateMap<IdentityAutenticarUsuarioResponse, UsuarioPermissaoDTO>();

            #endregion
        }
    }
}
