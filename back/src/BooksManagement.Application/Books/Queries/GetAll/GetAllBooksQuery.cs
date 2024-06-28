using AutoMapper;
using BooksManagement.Abstractions;
using BooksManagement.Application.Books.Commands;
using BooksManagement.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BooksManagement.Application.Books.Queries.GetAll
{
    public class GetAllBooksQuery : IRequest<IEnumerable<Book>>
    {
    }
}
