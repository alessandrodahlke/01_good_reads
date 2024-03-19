using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence.Repositories
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
