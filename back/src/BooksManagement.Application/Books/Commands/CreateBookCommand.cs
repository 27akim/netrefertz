﻿using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BooksManagement.Application.Books.Commands
{
    public class CreateBookCommand : IRequest<string>
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public required string ISBN { get; set; }
    }

    public class CreateBookHandler(IMapper mapper, IBookRepository repository, ILogger<CreateBookHandler> logger) : IRequestHandler<CreateBookCommand, string>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IBookRepository _repository = repository;
        private readonly ILogger<CreateBookHandler> _logger = logger;

        public async Task<string> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Book>(command);
            var entity = await _repository.AddAsync(book);
            return entity.Id;
        }
    }
}