import { Observable } from "rxjs";
import { Book } from "../models/book";

  export interface IBookService {
    getAllBooks(): Observable<Book[]>;
    getBookById(id: string): Observable<Book>;
    createBook(book: Book): Observable<string>;
    updateBook(book: Book): Observable<Book>;
    deleteBook(id: string): Observable<void>;
  }