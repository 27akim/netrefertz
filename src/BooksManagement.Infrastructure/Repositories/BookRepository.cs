using BooksManagement.Core;
using BooksManagement.Core.Entities;
using BooksManagement.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BooksManagement.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _dataContext;

        public BookRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Book> CreateAsync(Book book)
        {
            await _dataContext.Books.AddAsync(book);
            await _dataContext.SaveChangesAsync();
            return book;
        }

        public async Task DeleteAsync(Book book)
        {
            _dataContext.Books.Remove(book);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Book>> GetAllAsync()
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
