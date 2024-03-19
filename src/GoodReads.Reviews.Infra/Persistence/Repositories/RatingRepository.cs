using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IMongoCollection<Rating> _collection;

        public RatingRepository(IMongoContext context)
        {
            _collection = context.GetCollection<Rating>("ratings");
        }

        public async Task Add(Rating Rating)
        {
            await _collection.InsertOneAsync(Rating);
        }

        public async Task<Rating> GetById(string id)
        {
            return await _collection.Find(r => r.Id == id).SingleOrDefaultAsync();
        }

        public async Task Update(Rating Rating)
        {
            await _collection.ReplaceOneAsync(r => r.Id == Rating.Id, Rating);
        }

        public async Task<IEnumerable<Rating>> GetByBookId(string bookId)
        {
            return await _collection.Find(r => r.BookId == bookId).ToListAsync();
        }

        public async Task<IEnumerable<Rating>> GetByUserId(string userId)
        {
            return await _collection.Find(r => r.UserId == userId).ToListAsync();
        }

        public async Task<Rating> GetByUserAndBook(string userId, string bookId)
        {
            return await _collection.Find(r => r.UserId == userId && r.BookId == bookId).SingleOrDefaultAsync();
        }
    }
}
