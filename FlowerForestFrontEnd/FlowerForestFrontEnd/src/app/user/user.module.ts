import { HttpClientModule } from '@angular/common/http';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule } from '@angular/forms';

import { UserDetailsComponent } from './components/user-details/user-details.component';
import { AddUserComponent } from './components/add-user/add-user.component';
import { AuthenticateUserComponent } from './components/authenticate-user/authenticate-user.component';



@NgModule({
  declarations: [
    UserDetailsComponent,
    AddUserComponent,
    AuthenticateUserComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  exports: [
    AuthenticateUserComponent,
    AddUserComponent
  ]
})
export class UserModule { }
