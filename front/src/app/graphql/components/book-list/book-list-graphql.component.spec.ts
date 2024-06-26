import { TestBed, ComponentFixture } from '@angular/core/testing';
import { BookListComponentGraphQL } from './book-list-graphql.component';
import { BookServiceGraphQL } from '../../book-graphql.service';
import { Observable, of } from 'rxjs';
import { Book } from 'src/app/book/models/book';
import { RouterTestingModule } from '@angular/router/testing';

describe('BookListComponentGraphQL', () => {
  let component: BookListComponentGraphQL;
  let fixture: ComponentFixture<BookListComponentGraphQL>;
  let mockBookService: jasmine.SpyObj<BookServiceGraphQL>;

  beforeEach(() => {
    mockBookService = jasmine.createSpyObj('BookServiceGraphQL', ['getAllBooks']);

    TestBed.configureTestingModule({
      declarations: [ BookListComponentGraphQL ],
      providers: [
        { provide: BookServiceGraphQL, useValue: mockBookService }
      ],
      imports: [RouterTestingModule]
    });

    fixture = TestBed.createComponent(BookListComponentGraphQL);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load books on init', () => {
    const mockBooks: Book[] = [
      { id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' },
      { id: '2', title: 'Book 2', author: 'Author 2', publishedDate: new Date(), isbn: '0987654321' }
    ];
    mockBookService.getAllBooks.and.returnValue(of(mockBooks));

    fixture.detectChanges(); 

    expect(component.books$).toBeDefined();
    component.books$.subscribe((books) => {
      expect(books.length).toBe(2);
      expect(books).toEqual(mockBooks);
    });
  });
});
