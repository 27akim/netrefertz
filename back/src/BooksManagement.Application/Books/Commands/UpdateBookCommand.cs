using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Core.Entities;
using MediatR;

namespace BooksManagement.Application.Books.Commands
{
    public class UpdateBookCommand : IRequest
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public required string ISBN { get; set; }
    }

    public class UpdateBookHandler(IMapper mapper, IBookRepository repository) : IRequestHandler<UpdateBookCommand>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IBookRepository _repository = repository;

        public async Task Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(command);
            await _repository.UpdateAsync(book);
        }
    }
}
