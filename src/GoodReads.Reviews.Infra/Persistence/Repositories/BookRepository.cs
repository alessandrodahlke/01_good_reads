using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IMongoContext _context;

        private readonly IMongoCollection<Book> _collection;
        public BookRepository(IMongoContext context)
        {
            _context = context;
            _collection = context.GetCollection<Book>("books");
        }

        public async Task Add(Book book)
        {
            _context.AddCommand(() => _collection.InsertOneAsync(book));
        }


        public async Task<Book> GetById(string id)
        {
            return await _collection.Find(r => r.Id == id).SingleOrDefaultAsync();
        }

        public async Task Update(Book book)
        {
            _context.AddCommand(() => _collection.ReplaceOneAsync(r => r.Id == book.Id, book));
        }

        public void AddReview(string id, Review review)
        {
            var filter = Builders<Book>.Filter.Eq(r => r.Id, id);

            var definition = Builders<Book>.Update.Push(r => r.Reviews, review);

            _context.AddCommand(() => _collection.UpdateOneAsync(filter, definition));
        }

        public void AddRating(string id, Rating rating)
        {
            var filter = Builders<Book>.Filter.Eq(r => r.Id, id);

            var definition = Builders<Book>.Update.Push(r => r.Ratings, rating);

            _context.AddCommand(() => _collection.UpdateOneAsync(filter, definition));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
