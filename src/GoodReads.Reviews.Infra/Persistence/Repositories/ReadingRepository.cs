using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence.Repositories
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly IMongoCollection<Reading> _collection;

        public ReadingRepository(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<Reading>("readings");
        }

        public async Task Add(Reading reading)
        {
            await _collection.InsertOneAsync(reading);
        }

        public async Task<Reading> GetById(string id)
        {
            return await _collection.Find(r => r.Id == id).SingleOrDefaultAsync();
        }

        public async Task<List<Reading>> GetByBookId(Guid bookId)
        {
            return await _collection.Find(r => r.BookId == bookId).ToListAsync();
        }

        public async Task<List<Reading>> GetByUserId(Guid userId)
        {
            return await _collection.Find(r => r.UserId == userId).ToListAsync();
        }
    }
}
