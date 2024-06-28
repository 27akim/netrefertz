using BooksManagement.Api.GraphQL.Mutations;
using BooksManagement.Application.Books.Queries;
using BooksManagement.Application.Books.Queries.GetAll;
using BooksManagement.Application.Books.Queries.GetById;
using BooksManagement.Core.Entities;
using HotChocolate;
using MediatR;

namespace BooksManagement.Api.GraphQL.Queries
{
    public class BookQueries
    {
        public async Task<IEnumerable<Book>> GetBooksAsync([Service] IMediator mediator, [Service] ILogger<BookMutations> logger)
        {
            logger.LogInformation("{object}: {action}", nameof(BookMutations), nameof(GetBooksAsync));
            var result = await mediator.Send(new GetAllBooksQuery());
            return result;
        }

        public async Task<Book> GetBookByIdAsync([Service] IMediator mediator, [Service] ILogger<BookMutations> logger, string id)
        {
            logger.LogInformation("{object}: {action} : Id={id}", nameof(BookMutations), nameof(GetBookByIdAsync), id);
            var result = await mediator.Send(new GetBookByIdQuery { Id = id });
            return result;
        }
    }
}
