import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { MessageResponse } from 'src/app/shared/models/response';
import { UserDTOWithToken } from '../models/userDTOWithToken';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly requestPath: string = "https://localhost:44375/api/User/"
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  private token?: string = undefined;

  constructor(private http: HttpClient) { }

  authenticateUser(username: string, password: string): Observable<MessageResponse> {
    var response = this.http.post<MessageResponse>(this.requestPath + "Authenticate", { "username": username, "password": password }, this.httpOptions);
    response
      .subscribe(r => {
        this.token = (r.data as UserDTOWithToken).token;
        this.httpOptions.headers = this.httpOptions.headers.set("Authorization", `Bearer ${this.token}`);
      });

    return response;
  }

  getUserDetailsById(id: string): Observable<MessageResponse> {
    return this.http.get<MessageResponse>(`${this.requestPath}${id}`, this.httpOptions);
  }
}
