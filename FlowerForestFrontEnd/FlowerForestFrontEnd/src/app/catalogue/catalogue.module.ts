import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from '../shared/shared.module';

import { CatalogueListComponent } from './components/catalogue-list/catalogue-list.component';



@NgModule({
  declarations: [
    CatalogueListComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class CatalogueModule { }
