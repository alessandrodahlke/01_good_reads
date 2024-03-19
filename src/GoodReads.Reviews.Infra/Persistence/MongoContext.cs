using GoodReads.Reviews.Infra.Persistence.Repositories;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public MongoClient MongoClient { get; set; }

        private MongoConfig Settings { get; set; }

        public MongoContext(MongoConfig settings)
        {
            Settings = settings;
            ConfigureMongo();
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }

            MongoClient = new MongoClient(Settings.ConnectionString);

            Database = MongoClient.GetDatabase(Settings.Database);
        }


        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }
    }
}
