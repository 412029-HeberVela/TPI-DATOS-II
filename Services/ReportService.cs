using MongoDB.Bson;
using MongoDB.Driver;
using TPIntegrador.Data;
using TPIntegrador.DTOs;
using TPIntegrador.Models;

namespace TPIntegrador.Services
{
    public class ReportService
    {
        private readonly IMongoCollection<Pedido> _pedidos;
        public ReportService(MongoContext context)
        {
            _pedidos = context.Pedidos;
        }

        public async Task<List<ProductoMasVendidoDto>> GetProductosMasVendidos()
        {
            var pipeline = new[]
            {
            new BsonDocument("$unwind", "$productos"),
            new BsonDocument("$group", new BsonDocument
            {
                { "_id", "$productos.nombre" },
                { "cantidadVendida", new BsonDocument("$sum", "$productos.cantidad") }
            }),
            new BsonDocument("$sort", new BsonDocument("cantidadVendida", -1))
        };

            var result = await _pedidos.Aggregate<BsonDocument>(pipeline).ToListAsync();

            return result.Select(r => new ProductoMasVendidoDto
            {
                Producto = r["_id"].AsString,
                CantidadVendida = r["cantidadVendida"].AsInt32
            }).ToList();
        }

        public async Task<List<VentasPorDiaSemanaDto>> GetVentasPorDiaSemana()
        {
            var pipeline = new[]
            {
        new BsonDocument("$group", new BsonDocument
        {
            { "_id", new BsonDocument("$dayOfWeek", "$fecha") },
            { "cantidadPedidos", new BsonDocument("$sum", 1) },
            { "totalVentas", new BsonDocument("$sum", "$total") }
        }),
        new BsonDocument("$sort", new BsonDocument("_id", 1))
    };

            var result = await _pedidos.Aggregate<BsonDocument>(pipeline).ToListAsync();

            return result.Select(r => new VentasPorDiaSemanaDto
            {
                DiaNumero = r["_id"].AsInt32, // 1=Domingo, 7=Sábado
                CantidadPedidos = r["cantidadPedidos"].AsInt32,
                TotalVentas = r["totalVentas"].ToDecimal()
            }).ToList();
        }

        public async Task<List<VentasPorDiaDto>> GetVentasPorDia()
        {
            var pipeline = new[]
            {
        new BsonDocument("$group", new BsonDocument
        {
            { "_id", new BsonDocument("$dateToString", new BsonDocument
                {
                    { "format", "%Y-%m-%d" },
                    { "date", "$fecha" }
                })
            },
            { "totalVentas", new BsonDocument("$sum", "$total") }
        }),
        new BsonDocument("$sort", new BsonDocument("_id", 1))
        };

            var result = await _pedidos.Aggregate<BsonDocument>(pipeline).ToListAsync();

            return result.Select(r => new VentasPorDiaDto
            {
                Fecha = r["_id"].AsString,
                TotalVentas = r["totalVentas"].ToDecimal()
            }).ToList();
        }

        public async Task<List<PedidosPorClienteDto>> GetPedidosPorCliente()
        {
            var pipeline = new[]
            {
        new BsonDocument("$group", new BsonDocument
        {
            { "_id", "$cliente.nombre" },
            { "cantidadPedidos", new BsonDocument("$sum", 1) },
            { "totalGastado", new BsonDocument("$sum", "$total") }
        }),
        new BsonDocument("$sort", new BsonDocument("totalGastado", -1))
    };

            var result = await _pedidos.Aggregate<BsonDocument>(pipeline).ToListAsync();

            return result.Select(r => new PedidosPorClienteDto
            {
                Cliente = r["_id"].AsString,
                CantidadPedidos = r["cantidadPedidos"].AsInt32,
                TotalGastado = r["totalGastado"].ToDecimal()
            }).ToList();
        }

        public async Task<List<TopClienteDto>> GetTopClientes()
        {
            var pipeline = new[]
            {
        new BsonDocument("$group", new BsonDocument
        {
            { "_id", "$cliente.nombre" },
            { "cantidadPedidos", new BsonDocument("$sum", 1) },
            { "totalGastado", new BsonDocument("$sum", "$total") }
        }),
        new BsonDocument("$sort", new BsonDocument
        {
        { "cantidadPedidos", -1 },
        { "totalGastado", -1 }
    }),
        new BsonDocument("$limit", 5) // TOP 5
    };

            var result = await _pedidos.Aggregate<BsonDocument>(pipeline).ToListAsync();

            return result.Select(r => new TopClienteDto
            {
                Cliente = r["_id"].AsString,
                CantidadPedidos = r["cantidadPedidos"].AsInt32,
                TotalGastado = r["totalGastado"].ToDecimal()
            }).ToList();
        }

        public async Task<List<PedidosPorEstadoDto>> GetPedidosPorEstado()
        {
            var pipeline = new[]
            {
        new BsonDocument("$group", new BsonDocument
        {
            { "_id", "$estado" },
            { "cantidad", new BsonDocument("$sum", 1) }
        })
    };

            var result = await _pedidos.Aggregate<BsonDocument>(pipeline).ToListAsync();

            return result.Select(r => new PedidosPorEstadoDto
            {
                Estado = r["_id"].AsString,
                Cantidad = r["cantidad"].AsInt32
            }).ToList();
        }
    }
}
