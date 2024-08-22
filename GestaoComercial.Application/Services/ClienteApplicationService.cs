using GestaoComercial.Application.Commands;
using GestaoComercial.Application.Dtos;
using GestaoComercial.Application.Interfaces;
using GestaoComercial.Application.Validation;
using GestaoComercial.Domain.Entities;
using GestaoComercial.Domain.Interfaces;
using GestaoComercial.Infra.Redis.Interfaces;
using MediatR;
using StackExchange.Redis;
using System.Collections;

namespace GestaoComercial.Application.Services
{
    public class ClienteApplicationService : IClienteApplicationService
    {
        private readonly IMediator _mediator;
        private readonly IClienteRepository _clienteRepository;
        private readonly IRedisCacheService _redisCacheService;

        public ClienteApplicationService(IMediator mediator, IClienteRepository clienteRepository, IRedisCacheService redisCacheService)
        {
            _mediator = mediator;
            _clienteRepository = clienteRepository;
            _redisCacheService = redisCacheService;
        }

        public async Task<Result> Inserir(ClienteCreateCommand command)
        {
            return await _mediator.Send(command);
        }
        
        public async Task<Result> Alterar(ClienteUpdateCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<Result> Excluir(ClienteDeleteCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<Result<ICollection<ClienteDto>>> Consulta(string? nome = null)
        {
            var cacheKey = nome == null ? "listClientesAll" : $"listclientes_nome_={nome}";
            var query = await _redisCacheService.GetCacheAsync<ICollection<Cliente>>(cacheKey);

            if (query == null)
            {
                query = await _clienteRepository.Consultar(nome);
                await _redisCacheService.SetCacheAsync(cacheKey, query, TimeSpan.FromMinutes(10));
            }

            ICollection<ClienteDto> dto = query.Select(x => new ClienteDto
            {
                ClienteId = x.ClienteId,
                Nome = x.Nome,
                PedidoDtos = x.Pedidos?.Select(y => new PedidoDto
                {
                    PedidoId = y.PedidoId,
                    DataCriacao = y.DataCriacao
                }).ToList() ?? []
            }).ToList();

            return Result.Ok(dto);
        }
    }
}
