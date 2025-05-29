using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPedidoApi.Domain.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishAsync(string queue, object message);
    }
}
