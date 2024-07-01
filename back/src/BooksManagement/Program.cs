using BooksManagement.Api.Middlewares;
using BooksManagement.Api.Options;
using BooksManagement.Api.Extensions;
using BooksManagement.Application;
using BooksManagement.Infrastructure;
using BooksManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddWebApi(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
var corsOptions = builder.Configuration.GetSection(CORSOptions.CORSConfigs).Get<CORSOptions>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookDataContext>();
    await dbContext.Database.MigrateAsync();
}
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseCors(b => b.WithOrigins(corsOptions?.FrontendUrl).AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGraphQL("/graphql");

app.Run();
