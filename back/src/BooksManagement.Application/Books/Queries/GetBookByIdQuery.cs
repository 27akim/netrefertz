using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Application.Books.Commands;
using BooksManagement.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BooksManagement.Application.Books.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public required string Id { get; set; }
    }
}
