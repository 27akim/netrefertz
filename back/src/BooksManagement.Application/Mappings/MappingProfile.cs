using AutoMapper;
using BooksManagement.Application.Books.Commands.Create;
using BooksManagement.Application.Books.Commands.Update;
using BooksManagement.Core.Entities;

namespace BooksManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookCommand, Book>();
            CreateMap<UpdateBookCommand, Book>();
        }
    }
}
