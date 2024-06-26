import { Component, OnInit, Input } from '@angular/core';
import { BookServiceGraphQL } from '../../book-graphql.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subject, map, takeUntil } from 'rxjs';
import { Book } from 'src/app/book/models/book';

@Component({
  selector: 'app-book-detail-graphql',
  templateUrl: './book-detail-graphql.component.html',
  styleUrls: ['./book-detail-graphql.component.css']
})
export class BookDetailComponentGraphQL implements OnInit {
  @Input() bookId!: string;
  book$!: Observable<Book>;
  destroy$ = new Subject<boolean>();

  constructor(
    private bookService: BookServiceGraphQL,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = this.bookId ?? this.route.snapshot.paramMap.get('id');
    if (id) {
      this.book$ = this.bookService.getBookById(id).pipe(
        takeUntil(this.destroy$));
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

  deleteBook(): void {
    const id = this.bookId ?? this.route.snapshot.paramMap.get('id');
    if (id) {
      this.bookService.deleteBook(id).pipe(
        takeUntil(this.destroy$)
      ).subscribe(() => {
        this.router.navigate(['/graphql/books']);
      });
    }
  }
}
