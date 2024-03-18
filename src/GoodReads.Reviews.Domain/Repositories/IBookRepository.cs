using GoodReads.Reviews.Domain.Entities;

namespace GoodReads.Reviews.Domain.Repositories
{
    public interface IBookRepository
    {
        Task Add(Book book);
        Task<Book> GetById(string id);
        Task Update(Book book);
        void AddReview(string id, Review review);
        void AddRating(string id, Rating rating);

        Task<Book> GetReviewById(string reviewId);
        Task<Book> GetReviewByBookIdAndUserId(string bookId, string userId);
    }
}
