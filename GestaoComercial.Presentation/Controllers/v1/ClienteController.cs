using Asp.Versioning;
using GestaoComercial.Application.Commands;
using GestaoComercial.Application.Dtos;
using GestaoComercial.Application.Interfaces;
using GestaoComercial.Application.Services;
using GestaoComercial.Application.Validation;
using GestaoComercial.CrossCutting.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoComercial.Presentation.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteApplicationService _clienteApplicationService;

        public ClienteController(IClienteApplicationService clienteApplicationService)
        {
            _clienteApplicationService = clienteApplicationService;
        }

        /// <summary>
        /// Serviço para cadastrar Cliente.
        /// </summary>
        [ClaimsAuthorize("CustomizePermission", "CadastrarCliente")]
        [HttpPost]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Post([FromBody] ClienteCreateCommand command)
        {
            try
            {
                var result = await _clienteApplicationService.Inserir(command);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para alterar Cliente.
        /// </summary>
        [ClaimsAuthorize("CustomizePermission", "EditarCliente")]
        [HttpPut]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Put([FromBody] ClienteUpdateCommand command)
        {
            try
            {
                var result = await _clienteApplicationService.Alterar(command);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para excluir Cliente.
        /// </summary>
        [ClaimsAuthorize("CustomizePermission", "ExcluirCliente")]
        [HttpDelete]
        [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete([FromBody] ClienteDeleteCommand command)
        {
            try
            {
                var result = await _clienteApplicationService.Excluir(command);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

        /// <summary>
        /// Serviço para listar Cliente.
        /// </summary>
        [ClaimsAuthorize("CustomizePermission", "ListarCliente")]
        [HttpGet("GetAllCliente")]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCliente([FromQuery]string? nome = null)
        {
            try
            {
                //var result = await _clienteApplicationService.Consulta();
                //return Ok(result.Value.ToPagedList(request.CurrentPage, request.PageSize));
                return Ok(await _clienteApplicationService.Consulta(nome));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail(e.Message));
            }
        }

    }
}
