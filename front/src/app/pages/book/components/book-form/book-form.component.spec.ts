import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BookFormComponent } from './book-form.component';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, convertToParamMap } from '@angular/router';
import { of } from 'rxjs';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { BookRestService } from '../../services/book-rest.service';
import { BookServiceGraphQL } from '../../services/book-graphql.service';

describe('BookFormComponent', () => {
  let component: BookFormComponent;
  let fixture: ComponentFixture<BookFormComponent>;
  let mockBookRestService: jasmine.SpyObj<BookRestService>;
  let mockBookServiceGraphQL: jasmine.SpyObj<BookServiceGraphQL>;
  let mockActivatedRoute: any;
  let router: any;

  beforeEach(async () => {
    mockBookRestService = jasmine.createSpyObj('BookRestService', ['getBookById', 'updateBook', 'createBook']);
    mockBookServiceGraphQL = jasmine.createSpyObj('BookServiceGraphQL', ['getBookById', 'updateBook', 'createBook']);
    router = {
      navigate: jasmine.createSpy('navigate'),
      url: ""
    }
    mockActivatedRoute = {
      snapshot: {
        paramMap: {
          get: jasmine.createSpy('get').and.returnValue("1")
        }
      }
    };
    
    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, BookFormComponent],
      providers: [
        FormBuilder,
        { provide: BookRestService, useValue: mockBookRestService },
        { provide: BookServiceGraphQL, useValue: mockBookServiceGraphQL },
        { provide: ActivatedRoute, useValue: mockActivatedRoute },
        { provide: Router, useValue: router }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(BookFormComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should use BookRestService when apiType is "rest"', () => {
    Object.defineProperty(router, 'url', { value: 'rest/books/1', writable: false });
    mockBookRestService.updateBook.and.returnValue(of({ id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' }));
    mockBookRestService.getBookById.and.returnValue(of({ id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' }));
    fixture.detectChanges();
    expect(component.bookService).toBe(mockBookRestService);
  });

  it('should use BookServiceGraphQL when apiType is "graphql"', () => {
    Object.defineProperty(router, 'url', { value: 'graphql/books/1', writable: false });
    mockBookServiceGraphQL.updateBook.and.returnValue(of({ id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' }));
    mockBookServiceGraphQL.getBookById.and.returnValue(of({ id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' }));
    fixture.detectChanges();
    expect(component.bookService).toBe(mockBookServiceGraphQL);
  });

  it('should initialize book form with book data when bookId is present', () => {
    Object.defineProperty(router, 'url', { value: 'rest/books/1', writable: false });
    const bookMock = { id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' };
    mockBookRestService.getBookById.and.returnValue(of(bookMock));
    fixture.detectChanges();
    expect(component.bookService).toBe(mockBookRestService);
    expect(mockBookRestService.getBookById).toHaveBeenCalledWith('1');
    expect(component.bookForm.value).toEqual(bookMock);
  });

  it('should call updateBook when bookId is present on form submit', () => {
    Object.defineProperty(router, 'url', { value: 'rest/books/1', writable: false });
    component.bookForm.setValue({ id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' });
    mockBookRestService.getBookById.and.returnValue(of({ id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' }));
    mockBookRestService.updateBook.and.returnValue(of({ id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' }));
    fixture.detectChanges();
    component.onSubmit();
    expect(mockBookRestService.updateBook).toHaveBeenCalledWith(component.bookForm.value);
    expect(router.navigate).toHaveBeenCalledWith(['rest/books']);
  });
});
