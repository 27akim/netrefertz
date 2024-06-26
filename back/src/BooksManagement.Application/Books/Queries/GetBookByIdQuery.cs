using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Core.Entities;
using MediatR;

namespace BooksManagement.Application.Books.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public required string Id { get; set; }
    }

    public class GetBookByIdQueryHandler(IMapper mapper, IBookRepository repository) : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<Book> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(query.Id);
        }
    }
}
