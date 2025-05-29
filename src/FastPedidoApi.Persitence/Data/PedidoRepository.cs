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
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IMongoCollection<Pedido> _collection;

        public PedidoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Pedido>("Pedidos");
        }

        public async Task AddAsync(Pedido pedido) =>
            await _collection.InsertOneAsync(pedido);

        public async Task<List<Pedido>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<Pedido?> GetByIdAsync(string id) =>
             await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();


        public async Task UpdateStatusAsync(string id, PedidoStatus status) =>
            await _collection.UpdateOneAsync(
                Builders<Pedido>.Filter.Eq(p => p.Id, id),
                Builders<Pedido>.Update.Set(p => p.Status, status.ToString()) // Fix: Convert 'status' to string
            );
    }
}
