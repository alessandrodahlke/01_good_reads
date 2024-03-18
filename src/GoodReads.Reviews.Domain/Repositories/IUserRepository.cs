using GoodReads.Reviews.Domain.Entities;

namespace GoodReads.Reviews.Domain.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<User> GetById(string id);
        Task Update(User book);
        void AddBook(string id, Book book);
        void AddReview(string id, Review review);
        void AddRating(string id, Rating rating);
    }
}
