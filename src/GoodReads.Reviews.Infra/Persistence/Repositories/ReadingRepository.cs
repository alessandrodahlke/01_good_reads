using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence.Repositories
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly IMongoCollection<Reading> _collection;
        public ReadingRepository(IMongoContext context)
        {
            _collection = context.GetCollection<Reading>("readings");
        }

        public async Task Add(Reading Reading)
        {
            await _collection.InsertOneAsync(Reading);
        }

        public async Task<Reading> GetById(string id)
        {
            return await _collection.Find(r => r.Id == id).SingleOrDefaultAsync();
        }

        public async Task Update(Reading Reading)
        {
            await _collection.ReplaceOneAsync(r => r.Id == Reading.Id, Reading);
        }

        public async Task<IEnumerable<Reading>> GetByBookId(string bookId)
        {
            return await _collection.Find(r => r.BookId == bookId).ToListAsync();
        }

        public async Task<IEnumerable<Reading>> GetByUserId(string userId)
        {
            return await _collection.Find(r => r.UserId == userId).ToListAsync();
        }

        public async Task<Reading> GetByUserAndBook(string userId, string bookId)
        {
            return await _collection.Find(r => r.UserId == userId && r.BookId == bookId).SingleOrDefaultAsync();
        }
    }
}
