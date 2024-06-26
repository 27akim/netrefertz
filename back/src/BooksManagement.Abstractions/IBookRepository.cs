using BooksManagement.Core.Entities;

namespace BooksManagement.Abstractions
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetByIdAsync(string id);
    }
}
