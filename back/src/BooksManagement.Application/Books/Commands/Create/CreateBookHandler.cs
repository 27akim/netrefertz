using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BooksManagement.Application.Books.Commands.Create
{
    public class CreateBookHandler(IMapper mapper, IBookRepository repository, ILogger<CreateBookHandler> logger) : IRequestHandler<CreateBookCommand, string>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IBookRepository _repository = repository;
        private readonly ILogger<CreateBookHandler> _logger = logger;

        public async Task<string> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("{object}: {action}", nameof(CreateBookHandler), nameof(Handle));
            try
            {
                var book = _mapper.Map<Book>(command);
                var entity = await _repository.AddAsync(book);
                return entity.Id;
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {error} at {object}: {action}: ", ex.Message, nameof(CreateBookHandler), nameof(Handle));
                throw;
            }
        }
    }
}
