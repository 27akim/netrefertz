using BooksManagement.Core.Entities;
using BooksManagement.Core.Models.Requests;

namespace BooksManagement.Core.Interfaces.Services
{
    public interface IBookService
    {
        Task<IReadOnlyList<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(string id);
        Task<Book> CreateAsync(CreateBookRequest request);
        Task<Book> UpdateAsync(UpdateBookRequest request);
        Task<bool> DeleteAsync(string id);
    }
}
