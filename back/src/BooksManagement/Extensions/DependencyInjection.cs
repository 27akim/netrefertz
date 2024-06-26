using BooksManagement.Api.GraphQL.Mutations;
using BooksManagement.Api.GraphQL.Queries;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace BooksManagement.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Books API",
                    Version = "v1",
                    Description = "An API to perform operations regarding books"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
            });
            services.AddGraphQLServer()
                .AddQueryType<BookQueries>()
                .AddMutationType<BookMutations>()
                .AddProjections()
                .AddFiltering()
                .AddSorting();
            return services;
        }
    }
}
