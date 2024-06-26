using BooksManagement.Abstractions;
using MediatR;

namespace BooksManagement.Application.Books.Commands
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public required string Id { get; set; }
    }

    public class DeleteBookHandler(IBookRepository repository) : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBookRepository _repository = repository;

        public async Task<bool> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(command.Id);
            if (book == null)
            {
                return false;
            }
            await _repository.DeleteAsync(book);
            return true;
        }
    }
}
