import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookListComponent } from './book/components/book-list/book-list.component';
import { BookFormComponent } from './book/components/book-form/book-form.component';
import { BookDetailComponent } from './book/components/book-detail/book-detail.component';
import { BookListComponentGraphQL } from './graphql/components/book-list/book-list-graphql.component';
import { BookFormComponentGraphQL } from './graphql/components/book-form/book-form-graphql.component';
import { BookDetailComponentGraphQL } from './graphql/components/book-detail/book-detail-graphql.component';

const routes: Routes = [
  { path: '', redirectTo: '/rest/books', pathMatch: 'full' },
  { path: 'rest/books', component: BookListComponent },
  { path: 'rest/books/new', component: BookFormComponent },
  { path: 'rest/books/:id', component: BookDetailComponent },
  { path: 'rest/books/edit/:id', component: BookFormComponent },
  { path: 'graphql', redirectTo: '/graphql/books', pathMatch: 'full' },
  { path: 'graphql/books', component: BookListComponentGraphQL },
  { path: 'graphql/books/new', component: BookFormComponentGraphQL },
  { path: 'graphql/books/:id', component: BookDetailComponentGraphQL },
  { path: 'graphql/books/edit/:id', component: BookFormComponentGraphQL }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
