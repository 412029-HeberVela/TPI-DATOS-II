namespace TPIntegrador.Models
{
    public class Pedido
    {
        public string Id { get; set; }

        public string ClienteId { get; set; }
        public ClienteSnapshot Cliente { get; set; }

        public List<ItemPedido> Productos { get; set; }

        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
    }

    public class ClienteSnapshot
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
    }

    public class ItemPedido
    {
        public string ProductoId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
