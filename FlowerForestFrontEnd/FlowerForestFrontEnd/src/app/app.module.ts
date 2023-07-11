import { HeaderModule } from './header/header.module';
import { PlantsModule } from './plants/plants.module';

import { AppRoutingModule } from './routing/app-routing.module';

import { SharedModule } from './shared/shared.module';

import { ReactiveFormsModule } from '@angular/forms';

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    PlantsModule,
    SharedModule,
    AppRoutingModule,
    HeaderModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
