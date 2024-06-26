import { Component, OnInit, Input } from '@angular/core';
import { BookService } from '../../book.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Book } from '../../models/book';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})
export class BookDetailComponent implements OnInit {
  @Input() bookId!: string;
  book$!: Observable<Book>;
  destroy$ = new Subject<boolean>();

  constructor(
    private bookService: BookService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = this.bookId ?? this.route.snapshot.paramMap.get('id');
    if (id) {
      this.book$ = this.bookService.getBookById(id).pipe(
        takeUntil(this.destroy$)
      );
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
        this.router.navigate(['/rest/books']);
      });
    }
  }
}
