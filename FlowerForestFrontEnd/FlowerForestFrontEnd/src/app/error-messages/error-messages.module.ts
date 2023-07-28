import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataErrorMessageComponent } from './component/data-error-message/data-error-message.component';
import { FormErrorMessageComponent } from './component/form-error-message/form-error-message.component';



@NgModule({
  declarations: [
    DataErrorMessageComponent,
    FormErrorMessageComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    DataErrorMessageComponent,
    FormErrorMessageComponent
  ]
})
export class ErrorMessagesModule { }
