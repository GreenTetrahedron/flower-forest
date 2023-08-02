import { UserDashboardComponent } from '../views/components/user-dashboard/user-dashboard.component';
import { CatalogueDashboardComponent } from '../views/components/catalogue-dashboard/catalogue-dashboard.component';
import { CatalogueDetailsComponent } from '../views/components/catalogue-details/catalogue-details.component';
import { NotFoundComponent } from '../error-pages/components/not-found/not-found.component';
import { HomeComponent } from '../views/components/home/home.component';
import { SignInComponent } from '../views/components/sign-in/sign-in.component';
import { RegisterUserComponent } from '../views/components/register-user/register-user.component';

import { UnauthorizedComponent } from '../error-pages/components/unauthorized/unauthorized.component';
import { ForbiddenComponent } from '../error-pages/components/forbidden/forbidden.component';


import { RouterModule, Routes } from '@angular/router';


import { ReactiveFormsModule } from '@angular/forms';

import { NgModule } from '@angular/core';



const routes: Routes = [
  { path: "user/:userId/catalogue/:catalogueId", component: CatalogueDashboardComponent },
  { path: "user/:id", component: UserDashboardComponent },
  { path: "catalogue/:id", component: CatalogueDetailsComponent },
  { path: "sign-in", component: SignInComponent },
  { path: "register", component: RegisterUserComponent },
  { path: "home", component: HomeComponent },
  { path: "unauthorized", component: UnauthorizedComponent },
  { path: "forbidden", component: ForbiddenComponent },
  { path: "not-found", component: NotFoundComponent },
  { path: "", redirectTo: "/home", pathMatch: "full" },
  { path: "**", redirectTo: "/not-found" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation: "reload"}), ReactiveFormsModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
