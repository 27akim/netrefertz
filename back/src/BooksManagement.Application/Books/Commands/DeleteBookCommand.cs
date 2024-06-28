using BooksManagement.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BooksManagement.Application.Books.Commands
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public required string Id { get; set; }
    }
}
