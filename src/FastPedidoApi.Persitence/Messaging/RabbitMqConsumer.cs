using FastPedidoApi.Application.UseCases.ProcessPedido;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace FastPedidoApi.Persitence.Messaging
{
    public class RabbitMqConsumer
    {
        private readonly IConnection _connection;
        private readonly ProcessarPedido _processarPedido;

        public RabbitMqConsumer(IConnection connection, ProcessarPedido processarPedido)
        {
            _connection = connection;
            _processarPedido = processarPedido;
        }

        public void Start()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare("pedidos", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(message);
                    if (data != null && data.TryGetValue("Id", out var idElement))
                    {
                        var id = idElement.GetString();
                        if (!string.IsNullOrEmpty(id))
                        {
                            await Task.Delay(20000);
                            await _processarPedido.HandleAsync(id);
                            channel.BasicAck(ea.DeliveryTag, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            channel.BasicConsume("pedidos", false, consumer);
        }
    }

}
