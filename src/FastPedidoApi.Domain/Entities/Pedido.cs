using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FastPedidoApi.Domain.Entities
{
    public class Pedido
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nome_cliente")]
        public string NomeCliente { get; set; }

        [BsonElement("descricao")]
        public string Descricao { get; set; }

        [BsonElement("valor")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Valor { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; } = "pendente";

        [BsonElement("DataCriacao")]
        public DateTime? DataCriacao { get;  set; }
    }
}
