using FastPedidoAPI.Models;
using MongoDB.Driver;


namespace FastPedidoAPI.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Pedido> _pedidos;

        public MongoDBService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDB:Database"]);
            _pedidos = database.GetCollection<Pedido>("Pedidos");
        }

        public async Task<List<Pedido>> GetAll() => await _pedidos.Find(_ => true).ToListAsync();

        public async Task Create(Pedido pedido) => await _pedidos.InsertOneAsync(pedido);

        public async Task UpdateStatus(string id, string status) =>
            await _pedidos.UpdateOneAsync(
                Builders<Pedido>.Filter.Eq(p => p.Id, id),
                Builders<Pedido>.Update.Set(p => p.Status, status));
    }
}
