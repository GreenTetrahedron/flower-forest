import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgSelectOption } from '@angular/forms';
import { Observable, catchError, throwError } from 'rxjs';
import { TokenStorageService } from 'src/app/storage/services/token-storage/token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AppendTokenInterceptorService implements HttpInterceptor {

  constructor(private readonly tokenStorage: TokenStorageService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.tokenStorage.getToken();

    if (token != undefined && token?.length > 0) {
      var reqWithToken = req.clone({ headers: req.headers.set("Authorization", `Bearer ${token}`) });
      return next.handle(reqWithToken);
    }

    return next.handle(req);
  }

}
