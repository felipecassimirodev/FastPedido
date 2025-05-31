using FastPedidoApi.Domain.Interfaces;
using MongoDB.Bson.IO;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FastPedidoApi.Persitence.Data
{
    public class RabbitMqPublisher : IMessagePublisher
    {
        private readonly IConnection _connection;

        public RabbitMqPublisher(IConnection connection)
        {
            _connection = connection;
        }

        public Task PublishAsync(string queue, object message)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue, durable: false, exclusive: false, autoDelete: false);

            var body = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(message));
            channel.BasicPublish("", queue, null, body);

            return Task.CompletedTask;
        }
    }
}
