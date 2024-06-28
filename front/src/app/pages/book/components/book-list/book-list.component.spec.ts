import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, convertToParamMap } from '@angular/router';
import { of } from 'rxjs';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { BookRestService } from '../../services/book-rest.service';
import { BookServiceGraphQL } from '../../services/book-graphql.service';
import { BookListComponent } from './book-list.component';

describe('BookListComponent', () => {
  let component: BookListComponent;
  let fixture: ComponentFixture<BookListComponent>;
  let mockBookRestService: jasmine.SpyObj<BookRestService>;
  let mockBookServiceGraphQL: jasmine.SpyObj<BookServiceGraphQL>;
  let mockActivatedRoute: any;
  let router: any;

  beforeEach(async () => {
    mockBookRestService = jasmine.createSpyObj('BookRestService', ['getAllBooks']);
    mockBookServiceGraphQL = jasmine.createSpyObj('BookServiceGraphQL', ['getAllBooks']);
    router = {
      navigate: jasmine.createSpy('navigate'),
      url: "/rest"
    }
    mockActivatedRoute = {
      snapshot: {
        paramMap: {
          get: jasmine.createSpy('get').and.returnValue("1")
        }
      }
    };
    
    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, BookListComponent],
      providers: [
        FormBuilder,
        { provide: BookRestService, useValue: mockBookRestService },
        { provide: BookServiceGraphQL, useValue: mockBookServiceGraphQL },
        { provide: ActivatedRoute, useValue: mockActivatedRoute },
        { provide: Router, useValue: router }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(BookListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should use BookRestService when URL contains "rest"', () => {
    const booksMock = [{ id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' },
      { id: '2', title: 'Book 2', author: 'Author 2', publishedDate: new Date(), isbn: '0987654321' }];
    mockBookRestService.getAllBooks.and.returnValue(of(booksMock));
    component.ngOnInit();
    expect(component.apiType).toBe('rest');
    component.books$.subscribe(books => {
      expect(books).toEqual(booksMock);
    });
    expect(mockBookRestService.getAllBooks).toHaveBeenCalled();
  });

  it('should use BookServiceGraphQL when apiType is not "rest"', () => {
    router.url = 'graphql';
    const booksMock = [{ id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' },
      { id: '2', title: 'Book 2', author: 'Author 2', publishedDate: new Date(), isbn: '0987654321' }];
    mockBookServiceGraphQL.getAllBooks.and.returnValue(of(booksMock));
    component.ngOnInit();
    expect(component.apiType).toBe('graphql');
    component.books$.subscribe(books => {
      expect(books).toEqual(booksMock);
    });
    expect(mockBookServiceGraphQL.getAllBooks).toHaveBeenCalled();
  });
});
