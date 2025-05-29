using FastPedidoApi.Domain.Entities;
using FastPedidoApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPedidoApi.Application.UseCases.ProcessPedido
{
    public class ProcessarPedido
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ProcessarPedido(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task HandleAsync(string id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null)
            {
                Console.WriteLine($"⚠️ Pedido com ID {id} não encontrado.");
                return;
            }

            Console.WriteLine($"✅ Pedido encontrado: {pedido.NomeCliente} - {pedido.Descricao}");
            await _pedidoRepository.UpdateStatusAsync(id, PedidoStatus.Processado);
            Console.WriteLine("✅ Status atualizado para 'Processado'.");
        }

    }
}
