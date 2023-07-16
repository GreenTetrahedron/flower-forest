import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { MessageResponse } from 'src/app/shared/models/response';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly requestPath: string = "https://localhost:44375//api/Users/"
  private readonly httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  private token?: string = undefined;

  constructor(private http: HttpClient) { }
  
  authenticateUser(username: string, password: string): Observable<MessageResponse>{
    var response = this.http.post<MessageResponse>(this.requestPath, { "username": username, "password": password }, this.httpOptions);
    response
      .subscribe(r => this.token = (r.data as UserDTOWithToken).token);

    return response;
  }

}
