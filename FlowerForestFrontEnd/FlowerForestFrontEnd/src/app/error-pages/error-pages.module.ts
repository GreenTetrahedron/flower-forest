import { AppRoutingModule } from '../routing/app-routing.module';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NotFoundComponent } from './components/not-found/not-found.component';



@NgModule({
  declarations: [
    NotFoundComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule
  ]
})
export class ErrorPagesModule { }
