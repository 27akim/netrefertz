# BookManagement backend

## Introduction
BookManagement is an application for managing a list of books, allowing users to add, view, edit, and delete books. 
The solution is organized into the following projects:

1. **BooksManagement.Api**: Contains API controllers and the startup class for handling HTTP and GraphQL requests.
2. **BooksManagement.Application**: Contains the business logic of the API.
3. **BooksManagement.Core**: Contains entities used within the application.
4. **BooksManagement.Infrastructure.Abstractions**: Contains database interfaces used within the application.
5. **BooksManagement.Infrastructure**: Contains database context, migrations and implementation of the repository pattern.
6. **BooksManagement.Tests**: Contains unit tests.

## Prerequisites
To run the BookManagement application you will need:
1. **.NET 8.0.0**
2. **Visual Studio 2022**
3. **MS SQL Server**

## Running the Solution
1. Open the solution in Visual Studio 2022.
2. Run the `BooksManagement.Api`.

## API Endpoints
The API exposes the following endpoints:
1. **GET /api/books**: Returns all books.
2. **POST /api/books**: Creates new book.
3. **PUT /api/books**: Updates an existing book.
4. **GET /api/books/{id}**: Returns an existing book.
5. **DELETE /api/books/{id}**: Deletes an existing book.

## GraphQL
GraphQL endpoint:  
**/graphql**: Endpoint for graphql requests.
### Query and mutation examples
1. **Get all books query**  
```graphql
books {
    author
    id
    isbn
    publishedDate
    title
  }
```
2. **Get book by id query** 
```graphql
bookById(id: null) {
    author
    id
    isbn
    publishedDate
    title
  }
``` 
3. **Create book mutation**  
```graphql
createBook(
    command: { author: null, isbn: null, publishedDate: null, title: null }
  ) {
  }
``` 
4. **Update book mutation**  
```graphql
updateBook(  
    command: { author: null, id: null, isbn: null, publishedDate: null, title: null }  
  ) {  
    author  
    id  
    isbn  
    publishedDate  
    title  
  }  
``` 
5. **Delete book mutation**
```graphql
deleteBook(id: null) {
  }
```   

## What can be improved
1. **Result objects**: The solution can be enhanced by adding results objects to store the results of the operation, for example using the library FluentResults.
2. **Logging and Exception Handling**: The solution can be enhanced by adding more loggs and exception handling blocks.
3. **Pagination**:  The solution can be enhanced by adding pagination when getting all books.


# BookManagement frontend

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 18.0.6.

## Running the Solution

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

### Work with REST

To work with REST navigate to **/rest**

### Work with GraphQL

To work with GraphQL navigate to **/graphql**
