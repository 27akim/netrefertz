import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { BookListComponent } from './book/components/book-list/book-list.component';
import { BookDetailComponent } from './book/components/book-detail/book-detail.component';
import { BookFormComponent } from './book/components/book-form/book-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { GraphQLModule } from './graphql.module';
import { BookListComponentGraphQL } from './graphql/components/book-list/book-list-graphql.component';
import { BookDetailComponentGraphQL } from './graphql/components/book-detail/book-detail-graphql.component';
import { BookFormComponentGraphQL } from './graphql/components/book-form/book-form-graphql.component';

@NgModule({
  declarations: [
    AppComponent,
    BookListComponent,
    BookDetailComponent,
    BookFormComponent,
    BookListComponentGraphQL,
    BookDetailComponentGraphQL,
    BookFormComponentGraphQL
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule,
    GraphQLModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
