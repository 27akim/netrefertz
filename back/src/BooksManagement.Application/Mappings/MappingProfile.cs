using AutoMapper;
using BooksManagement.Application.Books.Commands;
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
