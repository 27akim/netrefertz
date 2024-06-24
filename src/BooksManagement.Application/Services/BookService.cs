using AutoMapper;
using BooksManagement.Core.Entities;
using BooksManagement.Core.Interfaces.Repositories;
using BooksManagement.Core.Interfaces.Services;
using BooksManagement.Core.Models.Requests;

namespace BooksManagement.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<Book> CreateAsync(CreateBookRequest request)
        {
            var book = _mapper.Map<Book>(request);
            return await _bookRepository.CreateAsync(book);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return false;
            }
            await _bookRepository.DeleteAsync(book);
            return true;
        }

        public async Task<IReadOnlyList<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetByIdAsync(string id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> UpdateAsync(UpdateBookRequest request)
        {
            var book = _mapper.Map<Book>(request);
            return await _bookRepository.UpdateAsync(book);
        }
    }
}
