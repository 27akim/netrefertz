import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Book } from '../models/book';
import { CREATE_BOOK, DELETE_BOOK, UPDATE_BOOK } from '../graphql/mutations/book.mutations';
import { GET_BOOKS, GET_BOOK_BY_ID } from '../graphql/queries/book.queries';
import { IBookService } from './book-service.interface';

@Injectable({
  providedIn: 'root'
})
export class BookServiceGraphQL implements IBookService {
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
  
    createBook(input: Book): Observable<string> {
      return this.apollo.mutate<{ createBook: string }>({
        mutation: CREATE_BOOK,
        variables: { command: { author: input.author, title: input.title, isbn: input.isbn, publishedDate: input.publishedDate } },
        refetchQueries: [{ query: GET_BOOKS }]
      })
      .pipe(
        map(result => {
          if (!result.data?.createBook) {
            throw new Error('Error creating book');
          }
          return result.data.createBook;
        }),
        catchError(this.handleError<string>('createBook'))
      );
    }
  
    updateBook(input: Book): Observable<Book> {
      return this.apollo.mutate<{ updateBook: Book }>({
        mutation: UPDATE_BOOK,
        variables: { command: input },
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
  
    deleteBook(id: string): Observable<void> {
      return this.apollo.mutate({
        mutation: DELETE_BOOK,
        variables: { id },
        refetchQueries: [{ query: GET_BOOKS }]
      })
      .pipe(
        map(() => void 0),
        catchError(this.handleError<void>('deleteBook'))
      );
    }

    private handleError<T>(operation = 'operation', result?: T) {
      return (error: any): Observable<T> => {
        console.error(error);
        return of(result as T);
      };
    }
  }

