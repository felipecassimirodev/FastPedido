using FastPedidoApi.Domain.Entities;
using FastPedidoApi.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPedidoApi.Persitence.Data
{
    public class MongoPedidoRepository : IPedidoRepository
    {
        private readonly IMongoCollection<Pedido> _collection;

        public MongoPedidoRepository(IMongoDatabase db)
        {
            _collection = db.GetCollection<Pedido>("Pedidos");
        }

        public async Task AddAsync(Pedido pedido)
        {
            await _collection.InsertOneAsync(pedido);
        }

        public async Task<List<Pedido>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Pedido?> GetByIdAsync(string id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateStatusAsync(string id, PedidoStatus status)
        {
            var filter = Builders<Pedido>.Filter.Eq(p => p.Id, id);
            var update = Builders<Pedido>.Update.Set(nameof(Pedido.Status), status.ToString().ToLower());
            await _collection.UpdateOneAsync(filter, update);
        }

    }
}
