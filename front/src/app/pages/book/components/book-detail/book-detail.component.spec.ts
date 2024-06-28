import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of, Subject } from 'rxjs';
import { BookDetailComponent } from './book-detail.component';
import { BookRestService } from '../../services/book-rest.service';
import { BookServiceGraphQL } from '../../services/book-graphql.service';
import { Book } from '../../models/book';
import { CommonModule } from '@angular/common';
import { RouterTestingModule } from '@angular/router/testing';

describe('BookDetailComponent', () => {
  let component: BookDetailComponent;
  let fixture: ComponentFixture<BookDetailComponent>;
  let bookRestService: jasmine.SpyObj<BookRestService>;
  let bookGraphQLService: jasmine.SpyObj<BookServiceGraphQL>;
  let route: ActivatedRoute;
  let router: Router;
  let mockBook: Book = { id: '1', title: 'Book 1', author: 'Author 1', publishedDate: new Date(), isbn: '1234567890' };

  beforeEach(async () => {
    const bookRestSpy = jasmine.createSpyObj('BookRestService', ['getBookById', 'deleteBook']);
    const bookGraphQLSpy = jasmine.createSpyObj('BookServiceGraphQL', ['getBookById', 'deleteBook']);

    await TestBed.configureTestingModule({
      imports: [CommonModule, BookDetailComponent],
      providers: [
        { provide: BookRestService, useValue: bookRestSpy },
        { provide: BookServiceGraphQL, useValue: bookGraphQLSpy },
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => '1' } } } }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(BookDetailComponent);
    component = fixture.componentInstance;
    bookRestService = TestBed.inject(BookRestService) as jasmine.SpyObj<BookRestService>;
    bookGraphQLService = TestBed.inject(BookServiceGraphQL) as jasmine.SpyObj<BookServiceGraphQL>;
    route = TestBed.inject(ActivatedRoute);
    router = TestBed.inject(Router);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize with REST API and fetch book by ID', () => {
    spyOnProperty(router, 'url', 'get').and.returnValue('/rest/book/1');
    bookRestService.getBookById.and.returnValue(of(mockBook));

    component.ngOnInit();

    expect(component.apiType).toBe('rest');
    expect(bookRestService.getBookById).toHaveBeenCalledWith('1');
    component.book$.subscribe(book => {
      expect(book).toEqual(mockBook);
    });
  });

  it('should initialize with GraphQL API and fetch book by ID', () => {
    spyOnProperty(router, 'url', 'get').and.returnValue('/graphql/book/1');
    bookGraphQLService.getBookById.and.returnValue(of(mockBook));

    component.ngOnInit();

    expect(component.apiType).toBe('graphql');
    expect(bookGraphQLService.getBookById).toHaveBeenCalledWith('1');
    component.book$.subscribe(book => {
      expect(book).toEqual(mockBook);
    });
  });
});
