import { Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: '/rest/books', pathMatch: 'full' },
  { path: 'rest/books', loadChildren: () => import('./pages/book/components/book-list/book-list.routes').then(m => m.BOOK_LIST_ROUTES) },
  { path: 'graphql/books', loadChildren: () => import('./pages/book/components/book-list/book-list.routes').then(m => m.BOOK_LIST_ROUTES) },
  { path: 'rest/books/new', loadChildren: () => import('./pages/book/components/book-form/book-form.routes').then(mod => mod.BOOK_FORM_ROUTES) },
  { path: 'rest/books/edit/:id', loadChildren: () => import('./pages/book/components/book-form/book-form.routes').then(mod => mod.BOOK_FORM_ROUTES) },
  { path: 'graphql/books/new', loadChildren: () => import('./pages/book/components/book-form/book-form.routes').then(mod => mod.BOOK_FORM_ROUTES) },
  { path: 'graphql/books/edit/:id', loadChildren: () => import('./pages/book/components/book-form/book-form.routes').then(mod => mod.BOOK_FORM_ROUTES) },
  { path: 'rest/books/:id', loadChildren: () => import('./pages/book/components/book-detail/book-detail.routes') .then(mod => mod.BOOK_DETAIL_ROUTES) },
  { path: 'graphql/books/:id', loadChildren: () => import('./pages/book/components/book-detail/book-detail.routes').then(mod => mod.BOOK_DETAIL_ROUTES) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

