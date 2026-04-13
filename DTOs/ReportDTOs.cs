namespace TPIntegrador.DTOs
{
    public class ReportDTOs
    {
    }
    public class ProductoMasVendidoDto
    {
        public string Producto { get; set; }
        public int CantidadVendida { get; set; }
    }
    public class VentasPorDiaSemanaDto
    {
        public int DiaNumero { get; set; }
        public int CantidadPedidos { get; set; }
        public decimal TotalVentas { get; set; }
    }
    public class VentasPorDiaDto
    {
        public string Fecha { get; set; }
        public decimal TotalVentas { get; set; }
    }
    public class PedidosPorClienteDto
    {
        public string Cliente { get; set; }
        public int CantidadPedidos { get; set; }
        public decimal TotalGastado { get; set; }
    }
    public class TopClienteDto
    {
        public string Cliente { get; set; }
        public int CantidadPedidos { get; set; }
        public decimal TotalGastado { get; set; }
    }
    public class PedidosPorEstadoDto
    {
        public string Estado { get; set; }
        public int Cantidad { get; set; }
    }
}
