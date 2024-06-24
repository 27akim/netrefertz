using BooksManagement.Core.Entities;

namespace BooksManagement.Core.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<IReadOnlyList<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(string id);
        Task<Book> CreateAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task DeleteAsync(Book book);
    }
}
