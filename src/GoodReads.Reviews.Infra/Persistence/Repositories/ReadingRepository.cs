using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence.Repositories
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly IMongoContext _context;

        private readonly IMongoCollection<Reading> _collection;
        public ReadingRepository(IMongoContext context)
        {
            _context = context;
            _collection = context.GetCollection<Reading>("reading");
        }

        public async Task Add(Reading review)
        {
            _context.AddCommand(() => _collection.InsertOneAsync(review));
        }

        public async Task<List<Reading>> GetByBookId(Guid bookId)
        {
            return await _collection.Find(r => r.BookId == bookId).ToListAsync();
        }

        public async Task<List<Reading>> GetByUserId(Guid userId)
        {
            return await _collection.Find(r => r.UserId == userId).ToListAsync();
        }

        public async Task<Reading> GetById(string id)
        {
            return await _collection.Find(r => r.Id == id).SingleOrDefaultAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
