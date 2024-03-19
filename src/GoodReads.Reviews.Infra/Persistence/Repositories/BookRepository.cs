using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<Book> _collection;
        public BookRepository(IMongoContext context)
        {
            _collection = context.GetCollection<Book>("books");
        }

        public async Task Add(Book book)
        {
            await _collection.InsertOneAsync(book);
        }

        public async Task<Book> GetById(string id)
        {
            return await _collection.Find(r => r.Id == id).SingleOrDefaultAsync();
        }

        public async Task Update(Book book)
        {
            await _collection.ReplaceOneAsync(r => r.Id == book.Id, book);
        }

        public async Task AddRating(string id, Rating rating)
        {
            var filter = Builders<Book>.Filter.Eq(r => r.Id, id);

            var definition = Builders<Book>.Update.Push(r => r.Ratings, rating);

            await _collection.UpdateOneAsync(filter, definition);
        }
    }
}
