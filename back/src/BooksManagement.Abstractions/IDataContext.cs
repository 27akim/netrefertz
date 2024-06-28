using BooksManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksManagement.Abstractions
{
    public interface IDataContext
    {
        DbSet<Book> Books { get; set; } 
        int SaveChanges();
    }
}
