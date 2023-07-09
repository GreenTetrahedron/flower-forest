import { PlantListComponent } from '../../plants/components/plant-list/plant-list.component';
import { PlantDetailsComponent } from '../../plants/components/plant-details/plant-details.component';

import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';



const routes: Routes = [
  { path: "", redirectTo: "/plants", pathMatch: "full" },
  { path: "plants", component: PlantListComponent },
  { path: "plant/:id", component: PlantDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
