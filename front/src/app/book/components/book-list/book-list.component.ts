import { Component, OnInit } from '@angular/core';
import { BookService } from '../../book.service';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Book } from '../../models/book';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  books$!: Observable<Book[]>;
  destroy$ = new Subject<boolean>();

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.books$ = this.bookService.getAllBooks().pipe(
      takeUntil(this.destroy$)
    );
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}
