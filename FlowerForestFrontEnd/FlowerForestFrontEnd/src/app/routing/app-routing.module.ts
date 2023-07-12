import { PlantListComponent } from '../plants/components/plant-list/plant-list.component';
import { PlantDetailsComponent } from '../plants/components/plant-details/plant-details.component';

import { RouterModule, Routes } from '@angular/router';

import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { AuthenticateUserComponent } from '../user/components/authenticate-user/authenticate-user.component';



const routes: Routes = [
  { path: "", redirectTo: "/plants", pathMatch: "full" },
  { path: "plants", component: PlantListComponent },
  { path: "plant/:id", component: PlantDetailsComponent },
  { path: "sign-in", component: AuthenticateUserComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), ReactiveFormsModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
