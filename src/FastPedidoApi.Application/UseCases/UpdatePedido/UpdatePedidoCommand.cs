using FastPedidoApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPedidoApi.Application.UseCases.UpdatePedido
{
    public record UpdatePedidoCommand(string id, PedidoStatus status);
}
