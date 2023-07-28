import { Injectable } from '@angular/core';


import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, catchError, map } from 'rxjs';

import { Catalogue } from '../models/catalogue';
import { MessageResponse } from 'src/app/shared/models/message-response';


@Injectable({
  providedIn: 'root'
})
export class CatalogueService {
  private readonly requestUrl: string = "https://localhost:44375/api/Catalogue";
  private httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json"
    })
  }

  constructor(private readonly http: HttpClient) { }


  getCatalogueById(id: string): Observable<Catalogue> {
    return this.http
      .get<MessageResponse>(`${this.requestUrl}/${id}`, this.httpOptions)
      .pipe(
        map((response: MessageResponse) => {
          return response.data as Catalogue;
        })
      );
  }

  getCataloguesByUserId(id: string): Observable<Catalogue[]> {
    return this.http
      .get<MessageResponse>(`${this.requestUrl}/User/${id}`, this.httpOptions)
      .pipe(
        map((response: MessageResponse) => {
          return response.data as Catalogue[]
        })
      );
  }

  getPublicCatalogues(): Observable<Catalogue[]> {
    return this.http
      .get<MessageResponse>(`${this.requestUrl}/Public`, this.httpOptions)
      .pipe(
        map((response: MessageResponse) => {
          const catalogues = response.data as Catalogue[];
          return catalogues;
        })
      );
  }

  addCatalogue(catalogue: Catalogue): Observable<Catalogue> {
    return this.http
      .post<MessageResponse>(this.requestUrl, JSON.stringify(catalogue), this.httpOptions)
      .pipe(
        map((response: MessageResponse) => {
          return response.data as Catalogue;
        })
      );
  }
  
  editCatalogue(catalogue: Catalogue): Observable<MessageResponse> {
    return this.http
      .put<MessageResponse>(this.requestUrl, JSON.stringify(catalogue), this.httpOptions);
  }
}
