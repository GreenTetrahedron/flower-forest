import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from '../routing/app-routing.module';

import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

import { ErrorMessagesModule } from '../error-messages/error-messages.module';
import { StorageModule } from '../storage/storage.module';


import { BackButtonComponent } from './components/back-button/back-button.component';


@NgModule({
  declarations: [
    BackButtonComponent,
  ],
  imports: [
    CommonModule
  ],
  exports: [
    BackButtonComponent,
    ErrorMessagesModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    StorageModule
  ]
})
export class SharedModule { }
