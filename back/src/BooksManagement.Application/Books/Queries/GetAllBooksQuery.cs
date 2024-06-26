using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Core.Entities;
using MediatR;

namespace BooksManagement.Application.Books.Queries
{
    public class GetAllBooksQuery : IRequest<IEnumerable<Book>>
    {
    }

    public class GetAllBooksQueryHandler(IMapper mapper, IBookRepository repository) : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>>
    {
        private readonly IBookRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
