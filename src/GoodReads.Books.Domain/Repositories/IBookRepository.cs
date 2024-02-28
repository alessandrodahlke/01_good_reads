using GoodReads.Books.Domain.Entities;
using GoodReads.Core.Communication;
using GoodReads.Core.Data;

namespace GoodReads.Books.Domain.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task Add(Book book);
        void Update(Book book);
        void Remove(Book book);
        Task<PagedResult<Book>> GetAll(int pageSize, int pageIndex);
        Task<Book> GetById(Guid id);
        Task<Book> GetByISBN(string isbn);
    }
}
