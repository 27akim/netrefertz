using BooksManagement.Abstractions;
using BooksManagement.Core.Entities;
using BooksManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BooksManagement.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDataContext _dataContext;
        private readonly DbSet<Book> _books;

        public BookRepository(BookDataContext dataContext)
        {
            _dataContext = dataContext;
            _books = dataContext.Books;
        }

        public async Task<Book> AddAsync(Book entity)
        {
            await _books.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Book book)
        {
            _books.Remove(book);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _books.AsNoTracking().ToListAsync();
        }

        public async Task<Book> GetByIdAsync(string id)
        {
            return await _books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            _dataContext.Entry(book).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
            return book;
        }
    }
}
