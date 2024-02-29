using GoodReads.Books.Application.DTO;
using GoodReads.Core.Results;

namespace GoodReads.Books.Application.Queries
{
    public interface IBookQueries
    {
        Task<PagedResult<BookDTO>> GetAll(int pageSize, int pageIndex);
        Task<BookDTO> GetById(Guid id);
    }
}
