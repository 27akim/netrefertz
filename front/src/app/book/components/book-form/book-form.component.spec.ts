import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { of } from 'rxjs';
import { BookFormComponent } from './book-form.component';
import { BookService } from '../../book.service';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';

class MockBookService {
  getBookById(id: string) {
    return of({ id, title: 'Test Title', author: 'Test Author', publishedDate: new Date(), isbn: '1234567890' });
  }
  updateBook(book: any) {
    return of(book);
  }
  createBook(book: any) {
    return of(book);
  }
}

describe('BookFormComponent', () => {
  let component: BookFormComponent;
  let fixture: ComponentFixture<BookFormComponent>;
  let bookService: BookService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, RouterTestingModule],
      declarations: [BookFormComponent],
      providers: [
        { provide: BookService, useClass: MockBookService },
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

    fixture = TestBed.createComponent(BookFormComponent);
    component = fixture.componentInstance;
    bookService = TestBed.inject(BookService);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load book data when bookId is present', () => {
    spyOn(bookService, 'getBookById').and.callThrough();
    component.ngOnInit();
    expect(bookService.getBookById).toHaveBeenCalledWith('1');
    expect(component.bookForm.value).toEqual({
      id: '1',
      title: 'Test Title',
      author: 'Test Author',
      publishedDate: jasmine.any(Date),
      isbn: '1234567890'
    });
  });

  it('should call updateBook with correct data when onSubmit is called with bookId', () => {
    spyOn(bookService, 'updateBook').and.callThrough();
    spyOn(router, 'navigate');
    component.bookId = '1';
    const updatedBook = {
      id: '1',
      title: 'Updated Title',
      author: 'Updated Author',
      publishedDate: new Date(),
      isbn: '0987654321'
    };
    component.bookForm.setValue(updatedBook);
    component.onSubmit();
    expect(bookService.updateBook).toHaveBeenCalledWith(updatedBook);
    expect(router.navigate).toHaveBeenCalledWith(['/rest/books']);
  });

  it('should call createBook with correct data when onSubmit is called without bookId', () => {
    spyOn(bookService, 'createBook').and.callThrough();
    spyOn(router, 'navigate');
    component.bookId = undefined;
    const newBook = {
      id: '',
      title: 'New Title',
      author: 'New Author',
      publishedDate: new Date(),
      isbn: '1122334455'
    };
    component.bookForm.setValue(newBook);
    component.onSubmit();
    expect(bookService.createBook).toHaveBeenCalledWith(newBook);
    expect(router.navigate).toHaveBeenCalledWith(['/rest/books']);
  });
});
