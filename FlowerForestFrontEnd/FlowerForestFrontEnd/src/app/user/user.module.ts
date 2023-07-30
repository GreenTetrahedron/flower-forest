import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule } from '@angular/forms';

import { SharedModule } from '../shared/shared.module';

import { UserDetailsComponent } from './components/user-details/user-details.component';
import { AddUserComponent } from './components/add-user/add-user.component';
import { AuthenticateUserComponent } from './components/authenticate-user/authenticate-user.component';
import { EditUserComponent } from './components/edit-user/edit-user.component';



@NgModule({
  declarations: [
    UserDetailsComponent,
    AddUserComponent,
    AuthenticateUserComponent,
    EditUserComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SharedModule,
  ],
  exports: [
    UserDetailsComponent,
    AddUserComponent,
    AuthenticateUserComponent
  ]
})
export class UserModule { }
