using Asp.Versioning;
using GestaoComercial.Application.Dtos;
using GestaoComercial.Application.Interfaces;
using GestaoComercial.Application.Models;
using GestaoComercial.Application.Services;
using GestaoComercial.Application.Validation;
using GestaoComercial.CrossCutting.Authorization;
using GestaoComercial.Infra.External.Identity.Model.Request;
using GestaoComercial.Infra.External.Identity.Model.Response;
using GestaoComercial.Presentation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoComercial.Presentation.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioApplicationService _usuarioApplicationService;
        private readonly JwtTokenService _jwtTokenService;

        public UsuarioController(IUsuarioApplicationService usuarioApplicationService, JwtTokenService jwtTokenService)
        {
            _usuarioApplicationService = usuarioApplicationService;
            _jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Serviço para logar o usuário
        /// </summary>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(Result<AutenticarUsuarioResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] AutenticarUsuarioRequestModel model)
        {
            try
            {
                var result = await _usuarioApplicationService.GetPermission(model);
                if (result != null && result.Permissoes.Count > 0)
                {
                    return Ok(Result.Ok(new AutenticarUsuarioResponseModel
                    {
                        UsuarioId = result.UsuarioId,
                        Email = result.Email,
                        Nome = result.Nome,
                        DataHoraAcesso = result.DataHoraAcesso,
                        AccessToken = _jwtTokenService.CreateToken(result)
                    }));                    
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, Result.Fail("Acesso negado!"));
                }            
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para obter informações do usuário no sistema
        /// </summary>
        [HttpGet("GetUsuario")]
        [ClaimsAuthorize("CustomizePermission", "InfoUsuarioIdentity")]
        [ProducesResponseType(typeof(Result<IdentityUsuarioPerfilSistemaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsuario([FromQuery] Guid sistemaId, Guid userId)
        {
            try
            {
                return Ok(await _usuarioApplicationService.GetIdentityUsuarioPorId(sistemaId, userId));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

    }
}
