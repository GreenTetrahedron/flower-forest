import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from './routing/app-routing.module';
import { BackButtonComponent } from './back-button/back-button.component';



@NgModule({
  declarations: [
    BackButtonComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    AppRoutingModule,
    BackButtonComponent
  ]
})
export class SharedModule { }
