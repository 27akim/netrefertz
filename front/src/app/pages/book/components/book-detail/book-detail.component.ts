import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Book } from '../../models/book';
import { BookRestService } from '../../services/book-rest.service';
import { BookServiceGraphQL } from '../../services/book-graphql.service';
import { IBookService } from '../../services/book-service.interface';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html',
  styleUrls: ['./book-detail.component.css']
})
export class BookDetailComponent implements OnInit {
  @Input() bookId!: string;
  book$!: Observable<Book>;
  destroy$ = new Subject<boolean>();
  apiType: string | undefined;
  bookService: IBookService | undefined;

  constructor(
    private bookRestService: BookRestService, 
    private bookGraphQLService: BookServiceGraphQL,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = this.bookId ?? this.route.snapshot.paramMap.get('id');
    this.apiType = this.router.url.includes("rest") ? "rest" : "graphql";
    if (this.apiType == "rest") {
      this.bookService = this.bookRestService;
    } 
    else {
      this.bookService = this.bookGraphQLService;
    }
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
      this.bookService?.deleteBook(id).pipe(
        takeUntil(this.destroy$)
      ).subscribe(() => {
        this.router.navigate([`${this.apiType}/books`]);
      });
    }
  }
}
