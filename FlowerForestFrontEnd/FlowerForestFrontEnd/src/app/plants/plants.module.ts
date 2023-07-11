import { AppRoutingModule } from '../routing/app-routing.module';
import { SharedModule } from '../shared/shared.module';

import { HttpClientModule } from '@angular/common/http';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PlantListComponent } from './components/plant-list/plant-list.component';
import { PlantDetailsComponent } from './components/plant-details/plant-details.component';


@NgModule({
  declarations: [
    PlantListComponent,
    PlantDetailsComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    SharedModule,
    AppRoutingModule
  ]
})
export class PlantsModule { }
