import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ApiInteractionsModule } from '../api-interactions/api-interactions.module';

import { BackButtonComponent } from './components/back-button/back-button.component';



@NgModule({
  declarations: [
    BackButtonComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    BackButtonComponent,
    ApiInteractionsModule
  ]
})
export class SharedModule { }
