import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';


import { UserDashboardComponent } from './components/user-dashboard/user-dashboard.component';

import { CatalogueModule } from '../catalogue/catalogue.module';
import { UserModule } from '../user/user.module';
import { PlantModule } from '../plant/plant.module';


import { CatalogueDashboardComponent } from './components/catalogue-dashboard/catalogue-dashboard.component';
import { HomeComponent } from './components/home/home.component';
import { CatalogueDetailsComponent } from './components/catalogue-details/catalogue-details.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { RegisterUserComponent } from './components/register-user/register-user.component';



@NgModule({
  declarations: [
    UserDashboardComponent,
    CatalogueDashboardComponent,
    HomeComponent,
    CatalogueDetailsComponent,
    SignInComponent,
    RegisterUserComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    CatalogueModule,
    UserModule,
    PlantModule
  ]
})
export class ViewsModule { }
