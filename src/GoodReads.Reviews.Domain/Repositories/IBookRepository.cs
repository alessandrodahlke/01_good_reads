using GoodReads.Reviews.Domain.Entities;

namespace GoodReads.Reviews.Domain.Repositories
{
    public interface IBookRepository
    {
        Task Add(Book book);
        Task<Book> GetById(string id);
        Task Update(Book book);
        void AddReview(string id, Review review);
    }
}
