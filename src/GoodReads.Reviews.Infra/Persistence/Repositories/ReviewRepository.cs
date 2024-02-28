using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IMongoCollection<Review> _collection;

        public ReviewRepository(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<Review>("reviews");
        }

        public async Task Add(Review review)
        {
            await _collection.InsertOneAsync(review);
        }

        public async Task<List<Review>> GetByBookId(Guid bookId)
        {
            return await _collection.Find(r => r.BookId == bookId).ToListAsync();
        }

        public async Task<Review> GetById(Guid id)
        {
            return await _collection.Find(r => r.Id == id).SingleOrDefaultAsync();
        }
    }
}
