using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IMongoContext _context;

        private readonly IMongoCollection<Review> _collection;
        public ReviewRepository(IMongoContext context)
        {
            _context = context;
            _collection = context.GetCollection<Review>("reviews");
        }

        public async Task Add(Review review)
        {
            _context.AddCommand(() => _collection.InsertOneAsync(review));
        }

        public async Task<List<Review>> GetByBookId(Guid bookId)
        {
            return await _collection.Find(r => r.BookId == bookId.ToString()).ToListAsync();
        }

        public async Task<Review> GetById(string id)
        {
            return await _collection.Find(r => r.Id == id).SingleOrDefaultAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
