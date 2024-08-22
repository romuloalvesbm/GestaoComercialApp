using AutoMapper;
using GestaoComercial.Application.Commands;
using GestaoComercial.Domain.Entities;
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
        }
    }
}
