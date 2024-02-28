using GoodReads.Reviews.Domain.Entities;

namespace GoodReads.Reviews.Domain.Repositories
{
    public interface IReviewRepository
    {
        Task Add(Review review);
        Task<Review> GetById(Guid id);
        Task<List<Review>> GetByBookId(Guid bookId);
    }
}
