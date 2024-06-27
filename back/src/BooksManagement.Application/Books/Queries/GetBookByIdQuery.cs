using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Application.Books.Commands;
using BooksManagement.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BooksManagement.Application.Books.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public required string Id { get; set; }
    }

    public class GetBookByIdQueryHandler(IMapper mapper, IBookRepository repository, ILogger<GetBookByIdQueryHandler> logger) : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<GetBookByIdQueryHandler> _logger = logger;

        public async Task<Book> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetByIdAsync(query.Id);
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {error} at {object}: {action}: ", ex.Message, nameof(CreateBookHandler), nameof(Handle));
                throw;
            }
        }
    }
}
