using FastPedidoApi.Domain.Entities;
using FastPedidoApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPedidoApi.Application.UseCases.CreatePedido
{
    public class CreatePedidoHandler
    {
        private readonly IPedidoRepository _repo;
        private readonly IMessagePublisher _publisher;

        public CreatePedidoHandler(IPedidoRepository repo, IMessagePublisher publisher)
        {
            _repo = repo;
            _publisher = publisher;
        }

        public async Task<string> Handle(CreatePedidoCommand command)
        {
            var pedido = new Pedido
            {
                NomeCliente = command.NomeCliente,
                Descricao = command.Descricao,
                Valor = command.Valor,
                Status = PedidoStatus.Pendente.ToString(),
                DataCriacao = DateTime.UtcNow
            };

            await _repo.AddAsync(pedido);
            await _publisher.PublishAsync("pedidos", pedido);

            return pedido.Id;
        }
    }
}
