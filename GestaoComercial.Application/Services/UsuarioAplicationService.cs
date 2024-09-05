using AutoMapper;
using GestaoComercial.Application.Interfaces;
using GestaoComercial.Application.Models;
using GestaoComercial.Application.Validation;
using GestaoComercial.Domain.Entities;
using GestaoComercial.Infra.External.Identity.Interfaces;
using GestaoComercial.Infra.External.Identity.Model.Request;
using GestaoComercial.Infra.External.Identity.Model.Response;
using GestaoComercial.Infra.Redis.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Services
{
    public class UsuarioAplicationService : IUsuarioApplicationService
    {
        private readonly ILoginService _loginService;
        private readonly IUsuarioPerfilSistemaService _usuarioPerfilSistemaService;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _redisCacheService;


        public UsuarioAplicationService(ILoginService loginService, IUsuarioPerfilSistemaService usuarioPerfilSistemaService, IMapper mapper, IRedisCacheService redisCacheService)
        {
            _loginService = loginService;
            _usuarioPerfilSistemaService = usuarioPerfilSistemaService;
            _mapper = mapper;
            _redisCacheService = redisCacheService;
        }

        public async Task<Result<IdentityUsuarioPerfilSistemaResponse?>> GetIdentityUsuarioPorId(Guid sistemaId, Guid userId)
        {
            var cacheKey = $"identityUsuario_{sistemaId}_{userId}";
            var query = await _redisCacheService.GetCacheAsync<IdentityUsuarioPerfilSistemaResponse>(cacheKey);

            if (query == null)
            {
                query = await _usuarioPerfilSistemaService.GetIdentityUsuarioPorId(sistemaId, userId);
                await _redisCacheService.SetCacheAsync(cacheKey, query, TimeSpan.FromMinutes(1));
            }
           
            return Result.Ok(query);
        }

        public async Task<Result<AutenticarUsuarioResponseModel>> GetToken(AutenticarUsuarioRequestModel model)
        {
            var result = await _loginService.GetToken(_mapper.Map<LoginIdentityRequest>(model));

            if (result != null)
                return Result.Ok(_mapper.Map<AutenticarUsuarioResponseModel>(result));
            else return Result.Fail<AutenticarUsuarioResponseModel>("Acesso Negado!");

        }
    }
}
