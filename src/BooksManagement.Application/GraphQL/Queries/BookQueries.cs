using BooksManagement.Core.Entities;
using BooksManagement.Core.Interfaces.Repositories;

namespace BooksManagement.Application.GraphQL.Queries
{
    public class BookQueries
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IReadOnlyList<Book>> GetBooksAsync([Service] IBookRepository repository)
        {
            return await repository.GetAllAsync();
        }

        public async Task<Book> GetBookByIdAsync([Service] IBookRepository repository, string id)
        {
            return await repository.GetByIdAsync(id);
        }
    }
}
