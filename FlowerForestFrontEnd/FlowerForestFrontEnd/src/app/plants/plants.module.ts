import { HttpClientModule } from '@angular/common/http';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PlantListComponent } from './components/plant-list/plant-list.component';
import { PlantDetailsComponent } from './components/plant-details/plant-details.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    PlantListComponent,
    PlantDetailsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    SharedModule
  ],
  exports: [
    PlantListComponent
  ]
})
export class PlantsModule { }
