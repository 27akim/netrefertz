using AutoMapper;
using BooksManagement.Core.Entities;
using BooksManagement.Core.Models.Requests;

namespace BooksManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookRequest, Book>();
            CreateMap<UpdateBookRequest, Book>();
        }
    }
}
