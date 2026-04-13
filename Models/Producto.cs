namespace TPIntegrador.Models
{
    public class Producto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public List<string> Extras { get; set; }
        public string Categoria { get; set; }
    }
}
