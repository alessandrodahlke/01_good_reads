using GoodReads.Reviews.Application.DTO;

namespace GoodReads.Reviews.Application.Queries
{
    public interface IBookQueries
    {
        Task<BookDTO> GetByIdAsync(Guid id);
    }
}
