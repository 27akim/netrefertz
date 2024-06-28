import { Component, OnInit } from '@angular/core';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Book } from '../../models/book';
import { ActivatedRoute, Router } from '@angular/router';
import { IBookService } from '../../services/book-service.interface';
import { BookRestService } from '../../services/book-rest.service';
import { BookServiceGraphQL } from '../../services/book-graphql.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {
  books$!: Observable<Book[]>;
  destroy$ = new Subject<boolean>();
  apiType: string | undefined;
  bookService: IBookService | undefined;
  constructor(private bookRestService: BookRestService, private bookGraphQLService: BookServiceGraphQL, private router: Router) { }

  ngOnInit(): void {
    this.apiType = this.router.url.includes("rest") ? "rest" : "graphql";
    if (this.apiType == "rest") {
      this.bookService = this.bookRestService;
    } 
    else {
      this.bookService = this.bookGraphQLService;
    }
    this.books$ = this.bookService.getAllBooks().pipe(
      takeUntil(this.destroy$)
    );
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}
