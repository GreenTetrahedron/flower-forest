import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserDetailsComponent } from './components/user-details/user-details.component';
import { AddUserComponent } from './components/add-user/add-user.component';



@NgModule({
  declarations: [
    UserDetailsComponent,
    AddUserComponent
  ],
  imports: [
    CommonModule
  ]
})
export class UserModule { }
