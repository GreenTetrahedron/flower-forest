import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, catchError, throwError } from 'rxjs';
import { TokenStorageService } from 'src/app/storage/services/token-storage/token-storage.service';
import { UserStorageService } from 'src/app/storage/services/user-storage/user-storage.service';

@Injectable({
  providedIn: 'root'
})
export class CatchErrorInterceptorService implements HttpInterceptor {
  constructor(private readonly router: Router,
    private readonly tokenStorage: TokenStorageService, private readonly userStorage: UserStorageService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          var resultantErrorMessage = "Something went wrong; please try again later...";

          switch (error.status) {
            case 401: {
              this.tokenStorage.clearToken();
              this.userStorage.clearUser();
              this.router.navigateByUrl("unauthorized");
              resultantErrorMessage = "Unauthorized access; Your session may have run out... Try signing in again..." + error.error;
              break;
            }
            case 403: {
              this.router.navigateByUrl("forbidden");
              resultantErrorMessage = "Forbidden access: " + error.error;
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
