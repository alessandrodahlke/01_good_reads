using GoodReads.Reviews.Domain.Entities;
using GoodReads.Reviews.Domain.Repositories;
using MongoDB.Driver;

namespace GoodReads.Reviews.Infra.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoContext _context;

        private readonly IMongoCollection<User> _collection;
        public UserRepository(IMongoContext context)
        {
            _context = context;
            _collection = context.GetCollection<User>("users");
        }

        public async Task Add(User User)
        {
            _context.AddCommand(() => _collection.InsertOneAsync(User));
        }

        public async Task<User> GetById(string id)
        {
            return await _collection.Find(r => r.Id == id).SingleOrDefaultAsync();
        }

        public async Task Update(User User)
        {
            _context.AddCommand(() => _collection.ReplaceOneAsync(r => r.Id == User.Id, User));
        }

        public void AddBook(string id, Book book)
        {
            var filter = Builders<User>.Filter.Eq(r => r.Id, id);

            var definition = Builders<User>.Update.Push(r => r.Books, book);

            _context.AddCommand(() => _collection.UpdateOneAsync(filter, definition));
        }

        public void AddReview(string id, Review review)
        {
            var filter = Builders<User>.Filter.Eq(r => r.Id, id);

            var definition = Builders<User>.Update.Push(r => r.Reviews, review);

            _context.AddCommand(() => _collection.UpdateOneAsync(filter, definition));
        }

        public void AddRating(string id, Rating rating)
        {
            var filter = Builders<User>.Filter.Eq(r => r.Id, id);

            var definition = Builders<User>.Update.Push(r => r.Ratings, rating);

            _context.AddCommand(() => _collection.UpdateOneAsync(filter, definition));
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
