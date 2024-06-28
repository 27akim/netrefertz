import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookFormComponent } from './book-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BookFormRoutingModule } from './book-form-routing.module';

@NgModule({
  declarations: [
    BookFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    BookFormRoutingModule
  ],
  exports: [
    BookFormComponent,
  ],
})
export class BookFormModule { }
