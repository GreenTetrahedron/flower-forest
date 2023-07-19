import { NotFoundComponent } from '../error-pages/components/not-found/not-found.component';

import { UserDetailsComponent } from '../user/components/user-details/user-details.component';
import { AuthenticateUserComponent } from '../user/components/authenticate-user/authenticate-user.component';


import { RouterModule, Routes } from '@angular/router';

import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';



const routes: Routes = [
  { path: "sign-in", component: AuthenticateUserComponent },
  { path: "user/details/:id", component: UserDetailsComponent },
  { path: "", redirectTo: "/plants", pathMatch: "full" },
  { path: "**", component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes), ReactiveFormsModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
