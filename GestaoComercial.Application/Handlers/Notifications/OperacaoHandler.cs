using GestaoComercial.Infra.Redis.Interfaces;
using GestaoComercial.Infra.Redis.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Handlers.Notifications
{
    public class OperacaoHandler : INotificationHandler<OperacaoNotification>
    {
        private readonly IRedisCacheService _redisCacheService;

        public OperacaoHandler(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }

        public async Task Handle(OperacaoNotification notification, CancellationToken cancellationToken)
        {
            // Aqui você pode adicionar lógica adicional para verificar o tipo de operação
            if (notification.Operacao == "Create" || notification.Operacao == "Update" || notification.Operacao == "Delete")
            {
                // Excluir a chave no Redis
                await _redisCacheService.DeleteKeysByPrefixAsync(notification.ChaveRedis);
                Console.WriteLine($"{notification.Operacao}: {notification.ChaveRedis}'");
            }
        }
    }
}
