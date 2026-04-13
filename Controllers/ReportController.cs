using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TPIntegrador.Data;
using TPIntegrador.Models;
using TPIntegrador.Services;

namespace TPIntegrador.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly ReportService _reportService;
        private readonly MongoContext _context;

        public ReportController(ReportService reportService, MongoContext context)
        {
            _reportService = reportService;
            _context = context;
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            var count = await _context.Pedidos.CountDocumentsAsync(FilterDefinition<Pedido>.Empty);
            return Ok($"Pedidos en la BD: {count}");
        }

        [HttpGet("productos-mas-vendidos")]
        public async Task<IActionResult> GetProductosMasVendidos()
        {
            var result = await _reportService.GetProductosMasVendidos();
            return Ok(result);
        }

        [HttpGet("ventas-por-dia-semana")]
        public async Task<IActionResult> GetVentasPorDiaSemana()
        {
            var result = await _reportService.GetVentasPorDiaSemana();
            return Ok(result);
        }

        [HttpGet("ventas-por-dia")]
        public async Task<IActionResult> GetVentasPorDia()
        {
            var result = await _reportService.GetVentasPorDia();
            return Ok(result);
        }

        [HttpGet("pedidos-por-cliente")]
        public async Task<IActionResult> GetPedidosPorCliente()
        {
            var result = await _reportService.GetPedidosPorCliente();
            return Ok(result);
        }

        [HttpGet("top-clientes")]
        public async Task<IActionResult> GetTopClientes()
        {
            var result = await _reportService.GetTopClientes();
            return Ok(result);
        }

        [HttpGet("pedidos-por-estado")]
        public async Task<IActionResult> GetPedidosPorEstado()
        {
            var result = await _reportService.GetPedidosPorEstado();
            return Ok(result);
        }
    }
}
