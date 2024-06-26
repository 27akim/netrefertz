import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { GET_BOOKS, GET_BOOK_BY_ID }  from './queries/book.queries';
import { CREATE_BOOK, UPDATE_BOOK, DELETE_BOOK }  from './mutations/book.mutations';
import { Book } from '../book/models/book';

@Injectable({
  providedIn: 'root'
})
export class BookServiceGraphQL {
    constructor(private apollo: Apollo) {}
  
    getAllBooks(): Observable<Book[]> {
      return this.apollo.watchQuery<{ books: Book[] }>({
        query: GET_BOOKS,
      })
      .valueChanges
      .pipe(
        map(result => result.data.books),
        catchError(this.handleError<Book[]>('getAllBooks', []))
      );
    }
  
    getBookById(id: string): Observable<Book> {
      return this.apollo.watchQuery<{ bookById: Book }>({
        query: GET_BOOK_BY_ID,
        variables: { id },
      }).valueChanges
      .pipe(
        map(result => result.data.bookById),
        catchError(this.handleError<Book>('getBookById'))
      );
    }
  
    createBook(input: Book): Observable<Book> {
      return this.apollo.mutate<{ createBook: Book }>({
        mutation: CREATE_BOOK,
        variables: { request: { author: input.author, title: input.title, isbn: input.isbn, publishedDate: input.publishedDate } },
        refetchQueries: [{ query: GET_BOOKS }]
      })
      .pipe(
        map(result => {
          if (!result.data?.createBook) {
            throw new Error('Error creating book');
          }
          return result.data.createBook;
        }),
        catchError(this.handleError<Book>('createBook'))
      );
    }
  
    updateBook(input: Book): Observable<Book> {
      return this.apollo.mutate<{ updateBook: Book }>({
        mutation: UPDATE_BOOK,
        variables: { request: input },
        refetchQueries: [{ query: GET_BOOKS }]
      })
      .pipe(
        map(result => {
          if (!result.data?.updateBook) {
            throw new Error('Error updating book');
          }
          return result.data.updateBook;
        }),
        catchError(this.handleError<Book>('updateBook'))
      );
    }
  
    deleteBook(id: string): Observable<boolean> {
      return this.apollo.mutate<{ deleteBook: boolean }>({
        mutation: DELETE_BOOK,
        variables: { id },
        refetchQueries: [{ query: GET_BOOKS }]
      })
      .pipe(
        map(result => {
          if (!result.data?.deleteBook) {
            throw new Error('Error deleting book');
          }
          return result.data.deleteBook;
        }),
        catchError(this.handleError<boolean>('deleteBook'))
      );
    }

    private handleError<T>(operation = 'operation', result?: T) {
      return (error: any): Observable<T> => {
        console.error(error);
        return of(result as T);
      };
    }
  }
