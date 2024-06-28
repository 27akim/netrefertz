import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BookListComponent } from './book-list.component';
import { BookRestService } from '../../services/book-rest.service';
import { BookServiceGraphQL } from '../../services/book-graphql.service';
import { of } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('BookListComponent', () => {
  let component: BookListComponent;
  let fixture: ComponentFixture<BookListComponent>;
  let mockBookRestService: jasmine.SpyObj<BookRestService>;
  let mockBookServiceGraphQL: jasmine.SpyObj<BookServiceGraphQL>;
  let mockRouter: any;

  beforeEach(async () => {
    mockBookRestService = jasmine.createSpyObj('BookRestService', ['getAllBooks']);
    mockBookServiceGraphQL = jasmine.createSpyObj('BookServiceGraphQL', ['getAllBooks']);
    mockRouter = {
        url: 'rest'
      };

    await TestBed.configureTestingModule({
      declarations: [ BookListComponent ],
      imports: [ ],
      providers: [
        { provide: BookRestService, useValue: mockBookRestService },
        { provide: BookServiceGraphQL, useValue: mockBookServiceGraphQL },
        { provide: Router, useValue: mockRouter }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should use BookRestService when apiType is "rest"', () => {
    mockBookRestService.getAllBooks.and.returnValue(of([]));
    fixture.detectChanges();
    expect(component.bookService).toBe(mockBookRestService);
  });

  it('should use BookServiceGraphQL when apiType is not "rest"', () => {
    mockRouter.url = 'graphql';
    mockBookServiceGraphQL.getAllBooks.and.returnValue(of([]));
    fixture.detectChanges();
    expect(component.bookService).toBe(mockBookServiceGraphQL);
  });

  it('should call getAllBooks and set books$ observable', () => {
    const booksMock = [{ id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' },
      { id: '2', title: 'Book 2', author: 'Author 2', publishedDate: new Date(), isbn: '0987654321' }];
    mockBookRestService.getAllBooks.and.returnValue(of(booksMock));
    fixture.detectChanges();
    component.books$.subscribe(books => {
      expect(books).toEqual(booksMock);
    });
    expect(mockBookRestService.getAllBooks).toHaveBeenCalled();
  });

  it('should call destroy$ subject on ngOnDestroy', () => {
    spyOn(component.destroy$, 'next');
    spyOn(component.destroy$, 'complete');
    component.ngOnDestroy();
    expect(component.destroy$.next).toHaveBeenCalledWith(true);
    expect(component.destroy$.complete).toHaveBeenCalled();
  });
});
