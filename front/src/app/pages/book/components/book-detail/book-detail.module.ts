import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookDetailRoutingModule } from './book-detail-routing.module';
import { BookDetailComponent } from './book-detail.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    BookDetailComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    BookDetailRoutingModule
  ],
  exports: [
    BookDetailComponent,
  ],
})
export class BookDetailModule { }
