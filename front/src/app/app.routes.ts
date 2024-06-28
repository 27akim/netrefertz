import { Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: '/rest/books', pathMatch: 'full' },
  { path: 'rest/books', loadChildren: () => import('./pages/book/components/book-list/book-list.module').then(m => m.BookListModule) },
  { path: 'graphql/books', loadChildren: () => import('./pages/book/components/book-list/book-list.module').then(m => m.BookListModule) },
  { path: 'rest/books/new', loadChildren: () => import('./pages/book/components/book-form/book-form.module').then(mod => mod.BookFormModule) },
  { path: 'rest/books/edit/:id', loadChildren: () => import('./pages/book/components/book-form/book-form.module').then(mod => mod.BookFormModule) },
  { path: 'graphql/books/new', loadChildren: () => import('./pages/book/components/book-form/book-form.module').then(mod => mod.BookFormModule) },
  { path: 'graphql/books/edit/:id', loadChildren: () => import('./pages/book/components/book-form/book-form.module').then(mod => mod.BookFormModule) },
  { path: 'rest/books/:id', loadChildren: () => import('./pages/book/components/book-detail/book-detail.module') .then(mod => mod.BookDetailModule) },
  { path: 'graphql/books/:id', loadChildren: () => import('./pages/book/components/book-detail/book-detail.module').then(mod => mod.BookDetailModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

