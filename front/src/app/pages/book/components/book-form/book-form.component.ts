import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { BookRestService } from '../../services/book-rest.service';
import { IBookService } from '../../services/book-service.interface';
import { BookServiceGraphQL } from '../../services/book-graphql.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  standalone: true,
  styleUrls: ['./book-form.component.css'],
  imports: [ReactiveFormsModule, CommonModule]
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
    this.apiType = this.router.url?.includes("rest") ? "rest" : "graphql";
    if (this.apiType == "rest") {
      this.bookService = this.bookRestService;
    } 
    else {
      this.bookService = this.bookGraphQLService;
    }
    if (this.bookId != undefined) {
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
