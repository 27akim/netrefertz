import { Component, OnInit } from '@angular/core';
import { BookServiceGraphQL } from '../../book-graphql.service';
import { Observable, Subject, map, takeUntil } from 'rxjs';
import { Book } from 'src/app/book/models/book';

@Component({
  selector: 'app-book-list-graphql',
  templateUrl: './book-list-graphql.component.html',
  styleUrls: ['./book-list-graphql.component.css']
})
export class BookListComponentGraphQL implements OnInit {
  books$!: Observable<Book[]>;
  destroy$ = new Subject<boolean>();

  constructor(private bookService: BookServiceGraphQL) { }

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
