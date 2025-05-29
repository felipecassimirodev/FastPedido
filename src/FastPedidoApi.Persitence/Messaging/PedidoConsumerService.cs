using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FastPedidoApi.Persitence.Messaging
{
    public class PedidoConsumerService : BackgroundService
    {
        private readonly RabbitMqConsumer _consumer;

        public PedidoConsumerService(RabbitMqConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Start();
            return Task.CompletedTask;
        }
    }
}
