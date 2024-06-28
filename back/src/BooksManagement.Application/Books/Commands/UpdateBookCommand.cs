using AutoMapper;
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
}
