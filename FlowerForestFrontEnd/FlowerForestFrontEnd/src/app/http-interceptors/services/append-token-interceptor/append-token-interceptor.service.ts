import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgSelectOption } from '@angular/forms';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppendTokenInterceptorService implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem("token");

    if (token != undefined && token?.length > 0) {
      var reqWithToken = req.clone({ headers: req.headers.set("Authorization", `Bearer ${token}`) });
      return next.handle(reqWithToken);
    }

    return next.handle(req);
  }

}
