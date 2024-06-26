import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Book } from './models/book';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiUrl = 'https://localhost:7178/api/books';

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

  createBook(book: Book): Observable<Book> {
    return this.http.post<Book>(this.apiUrl, book)
      .pipe(
        catchError(this.handleError<Book>('createBook'))
      );
  }

  updateBook(book: Book): Observable<Book> {
    return this.http.put<Book>(this.apiUrl, book)
      .pipe(
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
