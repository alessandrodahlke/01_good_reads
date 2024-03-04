using GoodReads.Reviews.Infra.Persistence.Repositories;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence
{
    public class MongoContext : IMongoContext
    {
        private readonly List<Func<Task>> _commands = new List<Func<Task>>();

        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }

        private MongoConfig Settings { get; set; }

        public MongoContext(MongoConfig settings)
        {
            Settings = settings;
            ConfigureMongo();

            //MongoClient = new MongoClient(settings.ConnectionString);
            //Database = MongoClient.GetDatabase(settings.Database);
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

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }


        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }

        public async Task<int> SaveChanges()
        {
            //ConfigureMongo();

            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }


        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
