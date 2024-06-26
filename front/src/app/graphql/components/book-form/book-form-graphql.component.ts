import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BookServiceGraphQL } from '../../book-graphql.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-book-form-graphql',
  templateUrl: './book-form-graphql.component.html',
  styleUrls: ['./book-form-graphql.component.css']
})
export class BookFormComponentGraphQL implements OnInit {
  @Input() bookId?: string;
  bookForm: FormGroup;
  destroy$ = new Subject<boolean>();

  constructor(
    private fb: FormBuilder,
    private bookService: BookServiceGraphQL,
    private router: Router,
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
    this.bookId = this.route.snapshot.paramMap.get('id') ?? undefined;;
    if (this.bookId) {
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
    this.bookService.getBookById(id).pipe(
      takeUntil(this.destroy$)
    ).subscribe(data => {
      this.bookForm.patchValue(data);
    });
  }

  private updateBook(): void {
    this.bookService.updateBook(this.bookForm.value).pipe(
      takeUntil(this.destroy$)
    ).subscribe(() => {
      this.router.navigate(['/graphql/books']);
    });
  }

  private createBook(): void {
    this.bookService.createBook(this.bookForm.value).pipe(
      takeUntil(this.destroy$)
    ).subscribe(() => {
      this.router.navigate(['/graphql/books']);
    });
  }
}