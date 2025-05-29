using FastPedidoApi.Application.UseCases.ProcessPedido;
using FastPedidoApi.Persitence.Data;
using FastPedidoApi.Persitence.Messaging;
using MongoDB.Driver;
using FastPedidoApi.Application.UseCases;


var mongoClient = new MongoClient("mongodb://localhost:27017/");
var database = mongoClient.GetDatabase("PedidosDb");

var pedidoRepository = new MongoPedidoRepository(database);
var processarPedido = new ProcessarPedido(pedidoRepository);
var consumer = new RabbitMqConsumer(processarPedido);

consumer.Start();
Console.ReadLine();
