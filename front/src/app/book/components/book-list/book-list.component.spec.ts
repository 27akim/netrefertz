import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BookListComponent } from './book-list.component';
import { BookService } from '../../book.service';
import { of } from 'rxjs';

class MockBookService {
  getAllBooks() {
    return of([
      { id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' },
      { id: '2', title: 'Book 2', author: 'Author 2', publishedDate: new Date(), isbn: '0987654321' }
    ]);
  }
}

describe('BookListComponent', () => {
  let component: BookListComponent;
  let fixture: ComponentFixture<BookListComponent>;
  let bookService: BookService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BookListComponent],
      providers: [{ provide: BookService, useClass: MockBookService }],
      imports: [RouterTestingModule]
    }).compileComponents();

    fixture = TestBed.createComponent(BookListComponent);
    component = fixture.componentInstance;
    bookService = TestBed.inject(BookService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load books on init', () => {
    spyOn(bookService, 'getAllBooks').and.callThrough();
    component.ngOnInit();
    expect(bookService.getAllBooks).toHaveBeenCalled();
    component.books$.subscribe((books) => {
      expect(books.length).toBe(2);
    });
  });
});
