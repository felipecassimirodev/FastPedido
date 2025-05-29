using FastPedidoApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPedidoApi.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task AddAsync(Pedido pedido);
        Task<List<Pedido>> GetAllAsync();
        Task<Pedido?> GetByIdAsync(string id);
        Task UpdateStatusAsync(string id, PedidoStatus status);
    }
}
