import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { PlantListComponent } from './components/plant-list/plant-list.component';
import { PlantDetailsComponent } from './components/plant-details/plant-details.component';
import { AddPlantComponent } from './components/add-plant/add-plant.component';
import { EditPlantComponent } from './components/edit-plant/edit-plant.component';



@NgModule({
  declarations: [
    PlantListComponent,
    PlantDetailsComponent,
    AddPlantComponent,
    EditPlantComponent,
    EditPlantComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    PlantListComponent,
    PlantDetailsComponent,
    AddPlantComponent,
    EditPlantComponent
  ]
})
export class PlantModule { }
