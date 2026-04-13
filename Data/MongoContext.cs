using MongoDB.Driver;
using TPIntegrador.Models;

namespace TPIntegrador.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Producto> Productos => _database.GetCollection<Producto>("productos");
        public IMongoCollection<Cliente> Clientes => _database.GetCollection<Cliente>("clientes");
        public IMongoCollection<Pedido> Pedidos => _database.GetCollection<Pedido>("pedidos");
    }
}
