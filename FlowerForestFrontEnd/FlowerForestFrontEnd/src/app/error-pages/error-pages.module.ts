import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from '../shared/shared.module';

import { NotFoundComponent } from './components/not-found/not-found.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';
import { ForbiddenComponent } from './components/forbidden/forbidden.component';



@NgModule({
  declarations: [
    NotFoundComponent,
    UnauthorizedComponent,
    ForbiddenComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class ErrorPagesModule { }
