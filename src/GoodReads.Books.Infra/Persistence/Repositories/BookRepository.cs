using GoodReads.Books.Domain.Entities;
using GoodReads.Books.Domain.Repositories;
using GoodReads.Core.Data;
using GoodReads.Core.Results;
using Microsoft.EntityFrameworkCore;

namespace GoodReads.Books.Infra.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksContext _context;
        public BookRepository(BooksContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task Add(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task<PagedResult<Book>> GetAll(int pageSize, int pageIndex)
        {
            var itens = await _context.Books
                .AsNoTracking()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var total = await _context.Books.CountAsync();
            return new PagedResult<Book>
            {
                Items = itens,
                TotalResults = total,
                PageSize = pageSize,
                PageIndex = pageIndex
            };
        }

        public async Task<Book> GetById(Guid id)
        {
            return await _context.Books.FirstOrDefaultAsync(b=>b.Id == id);
        }

        public async Task<Book> GetByISBN(string isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public void Remove(Book book)
        {
            _context.Books.Remove(book);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
