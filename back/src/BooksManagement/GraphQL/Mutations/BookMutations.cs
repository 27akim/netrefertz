using AutoMapper;
using BooksManagement.Application.Books.Commands;
using BooksManagement.Controllers;
using BooksManagement.Core.Entities;
using HotChocolate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BooksManagement.Api.GraphQL.Mutations
{
    public class BookMutations
    {
        public async Task<string> CreateBookAsync([Service] IMediator mediator, [Service] IMapper mapper, [Service] ILogger<BookMutations> logger, CreateBookCommand command)
        {
            logger.LogInformation("{action}", nameof(CreateBookAsync));
            var result = await mediator.Send(command);
            return result;
        }

        public async Task<bool> DeleteBookAsync([Service] IMediator mediator, [Service] ILogger<BookMutations> logger, string id)
        {
            logger.LogInformation("{action} : Id={id}", nameof(DeleteBookAsync), id);
            var result = await mediator.Send(new DeleteBookCommand { Id = id });
            return result;
        }

        public async Task UpdateBookAsync([Service] IMediator mediator, [Service] IMapper mapper, [Service] ILogger<BookMutations> logger, UpdateBookCommand command)
        {
            logger.LogInformation("{action}", nameof(UpdateBookAsync));
            await mediator.Send(command);
        }
    }
}
