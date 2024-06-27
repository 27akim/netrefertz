using AutoMapper;
using BooksManagement.Application.Books.Commands;
using BooksManagement.Core.Entities;
using HotChocolate;
using MediatR;

namespace BooksManagement.Api.GraphQL.Mutations
{
    public class BookMutations
    {
        public async Task<string> CreateBookAsync([Service] IMediator mediator, [Service] IMapper mapper, [Service] ILogger<BookMutations> logger, CreateBookCommand command)
        {
            logger.LogInformation("{object}: {action}",  nameof(BookMutations), nameof(CreateBookAsync));
            var result = await mediator.Send(command);
            return result;
        }

        public async Task<bool> DeleteBookAsync([Service] IMediator mediator, [Service] ILogger<BookMutations> logger, string id)
        {
            logger.LogInformation("{object}: {action} : Id={id}", nameof(BookMutations), nameof(DeleteBookAsync), id);
            var result = await mediator.Send(new DeleteBookCommand { Id = id });
            return result;
        }

        public async Task<Book> UpdateBookAsync([Service] IMediator mediator, [Service] IMapper mapper, [Service] ILogger<BookMutations> logger, UpdateBookCommand command)
        {
            logger.LogInformation("{object}: {action}", nameof(BookMutations), nameof(UpdateBookAsync));
            var result =  await mediator.Send(command);
            return result;
        }
    }
}
