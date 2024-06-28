using BooksManagement.Abstractions;
using BooksManagement.Application.Books.Commands.Create;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BooksManagement.Application.Books.Commands.Delete
{
    public class DeleteBookHandler(IBookRepository repository, ILogger<DeleteBookHandler> logger) : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBookRepository _repository = repository;
        private readonly ILogger<DeleteBookHandler> _logger = logger;

        public async Task<bool> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("{object}: {action}: Id={id}", nameof(CreateBookHandler), nameof(Handle), command.Id);
            try
            {
                var book = await _repository.GetByIdAsync(command.Id);
                if (book == null)
                {
                    return false;
                }
                await _repository.DeleteAsync(book);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {error} at {object}: {action}: ", ex.Message, nameof(CreateBookHandler), nameof(Handle));
                throw;
            }
        }
    }
}
