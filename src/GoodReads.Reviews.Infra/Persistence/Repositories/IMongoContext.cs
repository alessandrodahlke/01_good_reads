using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence.Repositories
{
    public interface IMongoContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        IMongoCollection<T> GetCollection<T>(string name);
    }

    
}
