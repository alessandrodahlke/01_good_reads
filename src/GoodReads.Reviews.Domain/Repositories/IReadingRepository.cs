using GoodReads.Reviews.Domain.Entities;

namespace GoodReads.Reviews.Domain.Repositories
{
    public interface IReadingRepository
    {
        Task Add(Reading review);
        Task<Reading> GetById(string id);
        Task<List<Reading>> GetByBookId(Guid bookId);
        Task<List<Reading>> GetByUserId(Guid userId);
    }
}
