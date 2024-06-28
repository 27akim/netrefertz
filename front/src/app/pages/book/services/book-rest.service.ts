import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Book } from '../models/book';
import { apiUrl } from '../../../app.config';
import { IBookService } from './book-service.interface';

@Injectable({
  providedIn: 'root'
})
export class BookRestService implements IBookService {
  private apiUrl = apiUrl + "/api/books";

  constructor(private http: HttpClient) { }

  getAllBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl)
      .pipe(
        catchError(this.handleError<Book[]>('getAllBooks', []))
      );
  }

  getBookById(id: string): Observable<Book> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Book>(url)
      .pipe(
        catchError(this.handleError<Book>('getBookById'))
      );
  }

  createBook(book: Book): Observable<string> {
    return this.http.post(this.apiUrl, book, { responseType: 'text' })
      .pipe(
        catchError(this.handleError<string>('createBook'))
      );
  }

  updateBook(book: Book): Observable<Book> {
    return this.http.put<Book>(this.apiUrl, book)
      .pipe(
        map(result => {
          return result;
        }),
        catchError(this.handleError<Book>('updateBook'))
      );
  }

  deleteBook(id: string): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url)
      .pipe(
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
