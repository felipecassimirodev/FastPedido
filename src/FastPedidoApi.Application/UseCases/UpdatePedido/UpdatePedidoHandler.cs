using FastPedidoApi.Application.UseCases.CreatePedido;
using FastPedidoApi.Domain.Entities;
using FastPedidoApi.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPedidoApi.Application.UseCases.UpdatePedido
{
    public class UpdatePedidoHandler
    {
        private readonly IPedidoRepository _repo;
        private readonly IMessagePublisher _publisher;

        public UpdatePedidoHandler(IPedidoRepository repo, IMessagePublisher publisher)
        {
            _repo = repo;
            _publisher = publisher;
        }

        public async Task<string> Handle(UpdatePedidoCommand command)
        {

            await _repo.UpdateStatusAsync(command.id , command.status);

            return command.id;
        }
        public async Task<bool> ProcessarPedidoAsync(string id)
        {
            var pedido = await _repo.GetByIdAsync(id);

            if (pedido == null)
            {
                Console.WriteLine($"⚠️ Pedido com ID {id} não encontrado.");
                return false;
            }

            await _repo.UpdateStatusAsync(id, PedidoStatus.Processado);
            Console.WriteLine($"✅ Pedido {id} atualizado.");
            return true;

        }

    }
}
