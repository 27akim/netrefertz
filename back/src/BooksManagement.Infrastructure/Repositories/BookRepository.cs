using BooksManagement.Abstractions;
using BooksManagement.Core.Entities;
using BooksManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BooksManagement.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDataContext _dataContext;

        public BookRepository(BookDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Book> AddAsync(Book entity)
        {
            await _dataContext.Books.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Book book)
        {
            _dataContext.Books.Remove(book);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _dataContext.Books.AsNoTracking().ToListAsync();
        }

        public async Task<Book> GetByIdAsync(string id)
        {
            return await _dataContext.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            _dataContext.Entry(book).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
            return book;
        }
    }
}
