import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CatchErrorInterceptorService implements HttpInterceptor {
  constructor(private readonly router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          var resultantErrorMessage = "Something went wrong; please try again later...";

          switch (error.status) {
            case 401: {
              this.router.navigateByUrl("unauthorized");
              resultantErrorMessage = "Unauthorized access: " + error.error;
              break;
            }
            case 404: {
              this.router.navigateByUrl("not-found");
              resultantErrorMessage = "Page not found: " + error.error;
              break;
            }
            default: {
              break;
            }
          };

          return throwError(() => new Error(resultantErrorMessage));
        })
      );
  }

}
