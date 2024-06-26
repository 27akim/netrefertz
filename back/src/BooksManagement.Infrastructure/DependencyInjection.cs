using BooksManagement.Abstractions;
using BooksManagement.Infrastructure.Data;
using BooksManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BooksManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<IDataContext, BookDataContext>(options => options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));
            services.AddScoped<IBookRepository, BookRepository>();

            return services;
        }
    }
}
