using AutoMapper;
using GestaoComercial.Application.Commands;
using GestaoComercial.Application.Handlers.Notifications;
using GestaoComercial.Application.Validation;
using GestaoComercial.Domain.Entities;
using GestaoComercial.Domain.Interfaces;
using GestaoComercial.Infra.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Handlers.Request
{
    public class ClienteRequestHandler : IRequestHandler<ClienteCreateCommand, Result>,
                                         IRequestHandler<ClienteUpdateCommand, Result>,
                                         IRequestHandler<ClienteDeleteCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ClienteRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Result> Handle(ClienteCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Verifica se o cliente já existe
                var clienteExistente = await _unitOfWork.ClienteRepository.ObterPorNome(request.Nome);
                if (clienteExistente != null)
                {
                    return Result.Fail("Cliente já cadastrado");
                }

                var cliente = _mapper.Map<Cliente>(request);
                await _unitOfWork.Begin();

                try
                {
                    await _unitOfWork.ClienteRepository.Inserir(cliente);
                    await _unitOfWork.Commit();
                    await _mediator.Publish(new OperacaoNotification { Operacao = "Create", ChaveRedis = "listClientesAll" });

                    return Result.Ok("Cliente cadastrado com sucesso");
                }
                catch
                {
                    await _unitOfWork.Rollback();
                    throw; // Lança a exceção original novamente para que possa ser tratada mais adiante
                }
            }
            catch (Exception ex)
            {
                return Result.Fail($"Ocorreu um erro ao cadastrar o cliente: {ex.Message}");
            }
            
        }

        public async Task<Result> Handle(ClienteUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {          

                // Verifica se o cliente já existe
                var clienteExistente = await _unitOfWork.ClienteRepository.ObterPorId(request.ClienteId);
                if (clienteExistente == null)
                {
                    return Result.Fail("Cliente não encontrado.");
                }

                var cliente = _mapper.Map<Cliente>(request);
                await _unitOfWork.Begin();

                try
                {
                    await _unitOfWork.ClienteRepository.Alterar(cliente);
                    await _unitOfWork.Commit();
                    await _mediator.Publish(new OperacaoNotification { Operacao = "Update", ChaveRedis = "listClientesAll" });
                    return Result.Ok("Cliente atualizado com sucesso");
                }
                catch
                {
                    await _unitOfWork.Rollback();
                    throw; // Lança a exceção original novamente para que possa ser tratada mais adiante
                }
            }
            catch (Exception ex)
            {
                return Result.Fail($"Ocorreu um erro ao atualizar o cliente: {ex.Message}");
            }
           
        }

        public async Task<Result> Handle(ClienteDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {              

                // Verifica se o cliente já existe
                var clienteExistente = await _unitOfWork.ClienteRepository.ObterPorId(request.ClienteId);
                if (clienteExistente == null)
                {
                    return Result.Fail("Cliente não encontrado.");
                }

                var cliente = _mapper.Map<Cliente>(request);
                await _unitOfWork.Begin();

                try
                {
                    await _unitOfWork.ClienteRepository.Excluir(cliente);
                    await _unitOfWork.Commit();
                    await _mediator.Publish(new OperacaoNotification { Operacao = "Delete", ChaveRedis = "listClientesAll" });
                    return Result.Ok("Cliente excluído com sucesso");
                }
                catch
                {
                    await _unitOfWork.Rollback();
                    throw; 
                }
            }
            catch (Exception ex)
            {
                return Result.Fail($"Ocorreu um erro ao excluir o cliente: {ex.Message}");
            }           
        }
    }
}
