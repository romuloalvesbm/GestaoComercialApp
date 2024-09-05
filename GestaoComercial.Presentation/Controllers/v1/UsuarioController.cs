using Asp.Versioning;
using GestaoComercial.Application.Dtos;
using GestaoComercial.Application.Interfaces;
using GestaoComercial.Application.Models;
using GestaoComercial.Application.Services;
using GestaoComercial.Application.Validation;
using GestaoComercial.Infra.External.Identity.Model.Request;
using GestaoComercial.Infra.External.Identity.Model.Response;
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

        public UsuarioController(IUsuarioApplicationService usuarioApplicationService)
        {
            _usuarioApplicationService = usuarioApplicationService;
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
                var result = await _usuarioApplicationService.GetToken(model);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para logar o usuário
        /// </summary>
        [HttpGet("GetUsuario")]
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
