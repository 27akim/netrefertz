import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { BookRestService } from '../../services/book-rest.service';
import { IBookService } from '../../services/book-service.interface';
import { BookServiceGraphQL } from '../../services/book-graphql.service';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css']
})
export class BookFormComponent implements OnInit {
  @Input() bookId?: string | undefined;
  bookForm: FormGroup;
  destroy$ = new Subject<boolean>();
  bookService: IBookService | undefined;
  apiType: string | undefined;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private bookRestService: BookRestService, 
    private bookGraphQLService: BookServiceGraphQL,
    private route: ActivatedRoute
  ) {
    this.bookForm = this.fb.group({
      id: [''],
      title: [''],
      author: [''],
      publishedDate: [''],
      isbn: ['']
    });
  }

  ngOnInit(): void {
    this.bookId = this.route.snapshot.paramMap.get('id') ?? undefined;
    console.log("1__________________________= " +  this.route.snapshot.paramMap.get('id'));
    this.apiType = this.router.url?.includes("rest") ? "rest" : "graphql";
    console.log("2__________________________= " +  this.apiType);
    if (this.apiType == "rest") {
      this.bookService = this.bookRestService;
      console.log("3__________________________= ");
    } 
    else {
      console.log("4__________________________= ");
      this.bookService = this.bookGraphQLService;
    }
    if (this.bookId != undefined) {
      console.log("5__________________________= " + this.bookId);
      this.getBook(this.bookId);
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

  onSubmit(): void {
    if (this.bookId) {
      this.updateBook();
    } else {
      this.createBook();
      console.log("__________________________=createBook " );
    }
  }

  private getBook(id: string): void {
    this.bookService?.getBookById(id).pipe(
      takeUntil(this.destroy$)
    ).subscribe(data => {
      this.bookForm.patchValue(data);
    });
  }

  private updateBook(): void {
    this.bookService?.updateBook(this.bookForm.value).pipe(
      takeUntil(this.destroy$)
    ).subscribe(() => {
      this.router.navigate([`${this.apiType}/books`]);
    });
  }

  private createBook(): void {
    this.bookService?.createBook(this.bookForm.value).pipe(
      takeUntil(this.destroy$)
    ).subscribe(() => {
      this.router.navigate([`${this.apiType}/books`]);
    });
  }
}
