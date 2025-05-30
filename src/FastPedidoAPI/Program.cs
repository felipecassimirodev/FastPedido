using FastPedidoApi.Application.UseCases.CreatePedido;
using FastPedidoApi.Application.UseCases.ProcessPedido;
using FastPedidoApi.Domain.Interfaces;
using FastPedidoApi.Persitence.Data;
using FastPedidoApi.Persitence.Messaging;
using FastPedidoAPI.Services;
using MongoDB.Driver;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MongoDB
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration["MongoDbSettings:ConnectionString"];

    return new MongoClient(connectionString);
});


builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var configuration = sp.GetRequiredService<IConfiguration>();
    var databaseName = configuration["MongoDbSettings:Database"];

    return client.GetDatabase(databaseName);
});

// Registro da conexão RabbitMQ
builder.Services.AddSingleton<IConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return RabbitMqConnectionFactory.CreateConnection(configuration);
});

builder.Services.AddScoped<IModel>(sp =>
{
    var connection = sp.GetRequiredService<IConnection>();
    return connection.CreateModel();
});


builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IMessagePublisher, RabbitMqPublisher>();
builder.Services.AddScoped<CreatePedidoHandler>();

builder.Services.AddSingleton<IPedidoRepository, MongoPedidoRepository>(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var database = mongoClient.GetDatabase("PedidosDb");
    return new MongoPedidoRepository(database);
});

builder.Services.AddSingleton<ProcessarPedido>();
builder.Services.AddSingleton<RabbitMqConsumer>();
builder.Services.AddHostedService<PedidoConsumerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
