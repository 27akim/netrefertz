import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BookDetailComponent } from './book-detail.component';
import { BookRestService } from '../../services/book-rest.service';
import { BookServiceGraphQL } from '../../services/book-graphql.service';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('BookDetailComponent', () => {
  let component: BookDetailComponent;
  let fixture: ComponentFixture<BookDetailComponent>;
  let mockBookRestService: jasmine.SpyObj<BookRestService>;
  let mockBookServiceGraphQL: jasmine.SpyObj<BookServiceGraphQL>;
  let mockActivatedRoute: any;
  let mockRouter: jasmine.SpyObj<Router>;
  //let mockRouter: Router;

  beforeEach(async () => {
    mockBookRestService = jasmine.createSpyObj('BookRestService', ['getBookById', 'deleteBook']);
    mockBookServiceGraphQL = jasmine.createSpyObj('BookServiceGraphQL', ['getBookById', 'deleteBook']);
    mockActivatedRoute = {
      snapshot: {
        paramMap: {
          get: jasmine.createSpy('get').and.returnValue('1')
        }
      }
    };
    
    mockRouter = jasmine.createSpyObj('Router', ['navigate'], { url: 'rest/books/1'});
    
    await TestBed.configureTestingModule({
      declarations: [BookDetailComponent],
      imports: [],
      providers: [
        { provide: BookRestService, useValue: mockBookRestService },
        { provide: BookServiceGraphQL, useValue: mockBookServiceGraphQL },
        { provide: ActivatedRoute, useValue: mockActivatedRoute },
        { provide: Router, useValue: mockRouter }
      ],
      schemas: [NO_ERRORS_SCHEMA] 
    }).compileComponents();

    fixture = TestBed.createComponent(BookDetailComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should use BookRestService when apiType is "rest"', () => {
    mockBookRestService.getBookById.and.returnValue(of({ id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' }));
    fixture.detectChanges();
    expect(component.bookService).toBe(mockBookRestService);
    component.book$.subscribe(book => {
      expect(book.id).toBe('1');
    });
  });

  it('should use BookServiceGraphQL when apiType is "graphql"', () => {
    Object.defineProperty(mockRouter, 'url', { value: 'graphql', writable: false });
    mockBookServiceGraphQL.getBookById.and.returnValue(of({ id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' }));
    fixture.detectChanges();
    expect(component.bookService).toBe(mockBookServiceGraphQL);
    component.book$.subscribe(book => {
      expect(book.id).toBe('1');
    });
  });

  it('should call getBookById and set book$ observable', () => {
    const bookMock = { id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' };
    mockBookRestService.getBookById.and.returnValue(of(bookMock));
    fixture.detectChanges();
    component.book$.subscribe(book => {
      expect(book).toEqual(bookMock);
    });
    expect(mockBookRestService.getBookById).toHaveBeenCalledWith('1');
  });

  it('should call destroy$ subject on ngOnDestroy', () => {
    spyOn(component.destroy$, 'next');
    spyOn(component.destroy$, 'complete');
    component.ngOnDestroy();
    expect(component.destroy$.next).toHaveBeenCalledWith(true);
    expect(component.destroy$.complete).toHaveBeenCalled();
  });
});
