using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Application.Books.Commands.Create;
using BooksManagement.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BooksManagement.Application.Books.Queries.GetAll
{
    public class GetAllBooksQueryHandler(IMapper mapper, IBookRepository repository, ILogger<GetAllBooksQueryHandler> logger) : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>>
    {
        private readonly IBookRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<GetAllBooksQueryHandler> _logger = logger;

        public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {error} at {object}: {action}: ", ex.Message, nameof(CreateBookHandler), nameof(Handle));
                throw;
            }
        }
    }
}
