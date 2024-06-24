using AutoMapper;
using BooksManagement.Core.Entities;
using BooksManagement.Core.Interfaces.Repositories;
using BooksManagement.Core.Models.Requests;

namespace BooksManagement.Application.GraphQL.Mutations
{
    public class BookMutations
    {
        public async Task<Book> CreateBookAsync([Service] IBookRepository repository, [Service] IMapper mapper, CreateBookRequest request)
        {
            var book = mapper.Map<Book>(request);
            return await repository.CreateAsync(book);
        }

        public async Task<bool> DeleteBookAsync([Service] IBookRepository repository, string id)
        {
            var book = await repository.GetByIdAsync(id);
            if (book == null)
            {
                return false;
            }
            await repository.DeleteAsync(book);
            return true;
        }

        public async Task<Book> UpdateBookAsync([Service] IBookRepository repository, [Service] IMapper mapper, UpdateBookRequest request)
        {
            var book = mapper.Map<Book>(request);
            return await repository.UpdateAsync(book);
        }
    }
}
