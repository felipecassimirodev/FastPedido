using FastPedidoAPI.Models;
using FastPedidoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using FastPedidoApi.Application.UseCases.CreatePedido;
using FastPedidoApi.Domain.Interfaces;

namespace FastPedidoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {

        private readonly CreatePedidoHandler _handler;
        private readonly IPedidoRepository _repository;
        private readonly MongoDBService _mongo;

        public PedidoController(CreatePedidoHandler handler, IPedidoRepository repository)
        {
            _handler = handler;
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreatePedidoCommand command)
        {
            var id = await _handler.Handle(command);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pedidos = await _repository.GetAllAsync();
            return Ok(pedidos);
        }

    }
}

