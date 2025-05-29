using FastPedidoApi.Application.UseCases.CreatePedido;
using FastPedidoApi.Application.UseCases.UpdatePedido;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FastPedidoApi.Persitence.Data
{
    public class RabbitConsumer
    {
        private readonly UpdatePedidoHandler _updatePedidoHandler;

        public RabbitConsumer(UpdatePedidoHandler updatePedidoHandler)
        {
            _updatePedidoHandler = updatePedidoHandler;
        }

        public void Start()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                DispatchConsumersAsync = true // permite async/await no consumer
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("pedidos", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 1, false); // 1 mensagem por vez (para garantir ordem/processamento único)

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"📩 Mensagem recebida: {message}");

                try
                {
                    var data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(message);

                    if (data == null || !data.TryGetValue("Id", out var idElement))
                    {
                        Console.WriteLine("❌ Campo 'Id' não encontrado na mensagem.");
                        channel.BasicNack(ea.DeliveryTag, false, false); // descarta a mensagem
                        return;
                    }

                    string id = idElement.GetString();
                    Console.WriteLine($"🔄 Processando pedido {id}...");

                    bool atualizado = await _updatePedidoHandler.ProcessarPedidoAsync(id);

                    if (atualizado)
                        channel.BasicAck(ea.DeliveryTag, false); // remove da fila
                    else
                        channel.BasicNack(ea.DeliveryTag, false, false); // descarta (não reenfileira)

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erro ao processar mensagem: {ex.Message}");
                    channel.BasicNack(ea.DeliveryTag, false, false); // descarta
                }
            };

            channel.BasicConsume("pedidos", autoAck: false, consumer);
            Console.WriteLine("🐇 Consumidor RabbitMQ rodando. Pressione Enter para sair.");
            Console.ReadLine();
        }
    }
}
