import { AppRoutingModule } from '../routing/app-routing.module';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HeaderComponent } from './components/header/header.component';
import { NavigationLinksComponent } from './components/navigation-links/navigation-links.component';
import { UserLinksComponent } from './components/user-links/user-links.component';



@NgModule({
  declarations: [
    NavigationLinksComponent,
    HeaderComponent,
    UserLinksComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule
  ],
  exports: [
    HeaderComponent
  ]
})
export class HeaderModule { }
