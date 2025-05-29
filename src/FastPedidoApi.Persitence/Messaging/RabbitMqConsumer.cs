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
        private readonly ProcessarPedido _processarPedido;
        private readonly string _rabbitHost;
        private readonly string _rabbitUser;
        private readonly string _rabbitPassword;

        public RabbitMqConsumer(ProcessarPedido processarPedido, IConfiguration configuration)
        {
            _processarPedido = processarPedido;

            _rabbitHost = configuration["RabbitMq:Host"] ?? "localhost";
            _rabbitUser = configuration["RabbitMq:Username"] ?? "guest";
            _rabbitPassword = configuration["RabbitMq:Password"] ?? "guest";
        }

        public void Start()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitHost,
                UserName = _rabbitUser,
                Password = _rabbitPassword
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("pedidos", false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume("pedidos", false, consumer); // <- altere para false

            consumer.Received += async (model, ea) =>
            {
                Console.WriteLine("🟡 Evento de recebimento disparado");

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Mensagem recebida: {message}");

                try
                {
                    var data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(message);
                    if (data != null && data.TryGetValue("Id", out var idElement))
                    {
                        var id = idElement.GetString();
                        if (!string.IsNullOrEmpty(id))
                        {
                            await Task.Delay(60000);

                            await _processarPedido.HandleAsync(id);
                            // ACK manual aqui!
                            ((EventingBasicConsumer)model).Model.BasicAck(ea.DeliveryTag, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
                    // Pode usar BasicNack aqui se quiser reprocessar depois
                }
            };

        }
    }
}
