import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';


import { Injectable } from '@angular/core';


import { Observable, catchError, map, throwError } from 'rxjs';


import { AuthenticationResult } from '../models/authenticationResult';
import { MessageResponse } from 'src/app/shared/models/message-response';

import { User } from '../models/user';
import { UserWithToken } from '../models/userWithToken';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly requestUrl: string = "https://localhost:44375/api/User";
  private httpOptions = {
    headers: new HttpHeaders({ 
      "Content-Type": "application/json"
    })
  };

  constructor(private readonly http: HttpClient) { }

  authenticateUser(username: string, password: string): Observable<AuthenticationResult> {
    return this.http
      .post<MessageResponse>(this.requestUrl + "/Authenticate", {"username": username, "password": password}, this.httpOptions)
      .pipe(
        map((response: MessageResponse) => {
          const userWithToken: UserWithToken = (response.data as UserWithToken);
          const user: User = userWithToken.user;
          const authenticationSuccess: boolean = (response.message === "SUCCESS_AUTHENTICATION_VALIDCREDENTIALS");
          
          if (authenticationSuccess === true) {
            localStorage.setItem("token", userWithToken.token);
          }

          return {
            authenticationSuccess: authenticationSuccess,
            user: user
          } as AuthenticationResult
        })
      );
  }

  getUserDetailsById(id: string): Observable<User> {
    return this.http
      .get<MessageResponse>(this.requestUrl + `/${id}`, this.httpOptions)
      .pipe(
        map((response: MessageResponse) => {
          const user: User = response.data as User;
          return user;
        })
      )
  }
}
