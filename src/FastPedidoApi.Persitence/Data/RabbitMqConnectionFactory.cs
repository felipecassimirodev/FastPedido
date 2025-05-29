using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPedidoApi.Persitence.Data
{
    public static class RabbitMqConnectionFactory
    {
        public static IConnection CreateConnection(IConfiguration configuration)
        {
            var factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMqSettings:Host"],
                UserName = configuration["RabbitMqSettings:Username"],
                Password = configuration["RabbitMqSettings:Password"]
            };
            return factory.CreateConnection();
        }
    }

}
