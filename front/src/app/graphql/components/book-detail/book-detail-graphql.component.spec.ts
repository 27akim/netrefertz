import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BookDetailComponentGraphQL } from './book-detail-graphql.component';
import { BookServiceGraphQL } from '../../book-graphql.service';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Book } from 'src/app/book/models/book';

class MockBookService {
  getBookById(id: string) {
    return of( { id, title: 'Test Title', author: 'Test Author', publishedDate: new Date(), isbn: '1234567890' } );
  }
  deleteBook(id: string) {
    return of(null);
  }
}

describe('BookDetailComponentGraphQL', () => {
  let component: BookDetailComponentGraphQL;
  let fixture: ComponentFixture<BookDetailComponentGraphQL>;
  let bookService: BookServiceGraphQL;
  let router: Router;
  let activatedRoute: ActivatedRoute;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule],
      declarations: [BookDetailComponentGraphQL],
      providers: [
        { provide: BookServiceGraphQL, useClass: MockBookService },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: {
                get: (key: string) => '1'
              }
            }
          }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(BookDetailComponentGraphQL);
    component = fixture.componentInstance;
    bookService = TestBed.inject(BookServiceGraphQL);
    router = TestBed.inject(Router);
    activatedRoute = TestBed.inject(ActivatedRoute);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load book data on init', () => {
    spyOn(bookService, 'getBookById').and.callThrough();
    component.ngOnInit();
    expect(bookService.getBookById).toHaveBeenCalledWith('1');
    const mockBook: Book = {
      id: '1',
      title: 'Test Title',
      author: 'Test Author',
      publishedDate: new Date,
      isbn: '1234567890'
    };
    component.book$.subscribe((book) => {
      expect(book).toEqual(mockBook);
    });
  });

  it('should delete book and navigate to /graphql/books on delete', () => {
    spyOn(bookService, 'deleteBook').and.callThrough();
    spyOn(router, 'navigate');
    component.book$ = of({
      id: '1',
      title: 'Test Title',
      author: 'Test Author',
      publishedDate: new Date(),
      isbn: '1234567890'
    });
    component.deleteBook();
    expect(bookService.deleteBook).toHaveBeenCalledWith('1');
    expect(router.navigate).toHaveBeenCalledWith(['/graphql/books']);
  });
});
