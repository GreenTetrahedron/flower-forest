import { CatalogueModule } from './catalogue/catalogue.module';
import { UserModule } from './user/user.module';
import { HeaderModule } from './header/header.module';

import { ViewsModule } from './views/views.module';


import { AppRoutingModule } from './routing/app-routing.module';

import { SharedModule } from './shared/shared.module';


import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';


import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppendTokenInterceptorService } from './http-interceptors/services/append-token-interceptor/append-token-interceptor.service';
import { CatchErrorInterceptorService } from './http-interceptors/services/catch-error-interceptor/catch-error-interceptor.service';


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    SharedModule,
    AppRoutingModule,
    HeaderModule,
    UserModule,
    CatalogueModule,
    ViewsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AppendTokenInterceptorService, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: CatchErrorInterceptorService, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
