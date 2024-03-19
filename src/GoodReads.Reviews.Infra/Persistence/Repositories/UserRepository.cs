using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;
        public UserRepository(IMongoContext context)
        {
            _collection = context.GetCollection<User>("users");
        }

        public async Task Add(User User)
        {
            await _collection.InsertOneAsync(User);
        }

        public async Task<User> GetById(string id)
        {
            return await _collection.Find(r => r.Id == id).SingleOrDefaultAsync();
        }

        public async Task Update(User User)
        {
            await _collection.ReplaceOneAsync(r => r.Id == User.Id, User);
        }
    }
}
