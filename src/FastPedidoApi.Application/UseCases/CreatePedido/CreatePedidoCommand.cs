using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPedidoApi.Application.UseCases.CreatePedido
{
    public record CreatePedidoCommand(string NomeCliente, string Descricao, decimal Valor);
}
