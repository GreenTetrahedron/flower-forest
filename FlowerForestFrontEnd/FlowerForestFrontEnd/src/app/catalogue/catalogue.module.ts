import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from '../shared/shared.module';

import { CatalogueListComponent } from './components/catalogue-list/catalogue-list.component';
import { AddCatalogueComponent } from './components/add-catalogue/add-catalogue.component';
import { EditCatalogueComponent } from './components/edit-catalogue/edit-catalogue.component';


@NgModule({
  declarations: [
    CatalogueListComponent,
    AddCatalogueComponent,
    EditCatalogueComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    CatalogueListComponent,
    AddCatalogueComponent,
    EditCatalogueComponent
  ]
})
export class CatalogueModule { }
