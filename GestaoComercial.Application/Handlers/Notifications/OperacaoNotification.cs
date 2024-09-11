using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoComercial.Application.Handlers.Notifications
{
    public class OperacaoNotification : INotification
    {
        public string Operacao { get; set; } = string.Empty;
        public string ChaveRedis { get; set; } = string.Empty;
    }
}
