import { MessageResponse } from '../models/message-response';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiInteractionsService {
  private readonly apiUrl: string = "https://localhost:44375/api";
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  private token?: string;

  constructor(private http: HttpClient) { }

  setToken(token: string) {
    this.token = token;
    this.httpOptions.headers = this.httpOptions.headers.set('Authorization', `Bearer ${this.token}`);
  }

  private createUrlWithSlash(url?: string): string {
    return (url != undefined && url != null && url!.length > 0)? `/${url}` : '';
  }
  
  private createRouteParamsUrl(routeParams?: string[]): string {
    var routeParamsUrl: string = "";
    routeParams?.forEach(r => routeParamsUrl = `${routeParamsUrl}${this.createUrlWithSlash(r)}`);

    return routeParamsUrl;
  }

  getFromApi(controller: string, routeParams?: string[], action?: string): Observable<MessageResponse> {
    var routeParamsUrl: string = this.createRouteParamsUrl(routeParams);
    var actionUrl: string = this.createUrlWithSlash(action);

    return this.http.get<MessageResponse>(`${this.apiUrl}/${controller}${actionUrl}${routeParamsUrl}`, this.httpOptions);
  }

  postToApi(controller: string, body?: any, action?: string, routeParams?: string[]): Observable<MessageResponse> {
    var routeParamsUrl: string = this.createRouteParamsUrl(routeParams);
    var actionUrl: string = this.createUrlWithSlash(action);

    return this.http.post<MessageResponse>(`${this.apiUrl}/${controller}${actionUrl}${routeParamsUrl}`, body, this.httpOptions);
  }
}
