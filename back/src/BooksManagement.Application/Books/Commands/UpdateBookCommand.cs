﻿using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BooksManagement.Application.Books.Commands
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public required string ISBN { get; set; }
    }

    public class UpdateBookHandler(IMapper mapper, IBookRepository repository, ILogger<UpdateBookHandler> logger) : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IBookRepository _repository = repository;
        private readonly ILogger<UpdateBookHandler> _logger = logger;

        public async Task<Book> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var book = _mapper.Map<Book>(command);
                return await _repository.UpdateAsync(book);
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {error} at {object}: {action}: ", ex.Message, nameof(CreateBookHandler), nameof(Handle));
                throw;
            }
        }
    }
}
