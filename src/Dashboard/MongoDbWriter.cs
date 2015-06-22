using System.Threading.Tasks;
using Dashboard.ReadModel.Views;
using MongoDB.Driver;

namespace Dashboard
{
    public interface IMongoDbWriter
    {
        Task AddOrUpdate(UpdateProjectScore command);
    }

    public class MongoDbWriter : IMongoDbWriter
    {
        private readonly string _connectionString;
        private readonly string _databaseName;
        private static IMongoCollection<RagWidgetView> _collection;

        public MongoDbWriter(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        public async Task AddOrUpdate(UpdateProjectScore command)
        {
            var item = new RagWidgetView
            {
                Id = command.Id,
                Red = command.Red,
                Yellow = command.Yellow,
                Green = command.Green
            };
            var collection = GetCollection();
            await collection.ReplaceOneAsync(
                doc => doc.Id == command.Id,
                item,
                new UpdateOptions { IsUpsert = true });
        }

        private IMongoCollection<RagWidgetView> GetCollection()
        {
            if (_collection != null) return _collection;
            var client = new MongoClient(_connectionString);
            var database = client.GetDatabase(_databaseName);
            _collection = database.GetCollection<RagWidgetView>(typeof(RagWidgetView).Name);
            return _collection;
        }
    }
}