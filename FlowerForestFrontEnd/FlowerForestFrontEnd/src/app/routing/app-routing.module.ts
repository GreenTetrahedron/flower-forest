import { UserDashboardComponent } from '../views/components/user-dashboard/user-dashboard.component';
import { CatalogueDashboardComponent } from '../views/components/catalogue-dashboard/catalogue-dashboard.component';
import { CatalogueDetailsComponent } from '../views/components/catalogue-details/catalogue-details.component';

import { AuthenticateUserComponent } from '../user/components/authenticate-user/authenticate-user.component';

import { UnauthorizedComponent } from '../error-pages/components/unauthorized/unauthorized.component';
import { NotFoundComponent } from '../error-pages/components/not-found/not-found.component';


import { RouterModule, Routes } from '@angular/router';


import { ReactiveFormsModule } from '@angular/forms';

import { NgModule } from '@angular/core';
import { HomeComponent } from '../views/components/home/home.component';



const routes: Routes = [
  { path: "user/:userId/catalogue/:catalogueId", component: CatalogueDashboardComponent },
  { path: "user/:id", component: UserDashboardComponent },
  { path: "catalogue/:id", component: CatalogueDetailsComponent },
  { path: "sign-in", component: AuthenticateUserComponent },
  { path: "home", component: HomeComponent },
  { path: "unauthorized", component: UnauthorizedComponent },
  { path: "not-found", component: NotFoundComponent },
  { path: "", redirectTo: "/home", pathMatch: "full" },
  { path: "**", redirectTo: "/not-found" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation: "reload"}), ReactiveFormsModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
