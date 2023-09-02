import { HttpClient, HttpHeaders } from '@angular/common/http';


import { Injectable } from '@angular/core';


import { Observable, map } from 'rxjs';

import { Plant } from '../models/plant';
import { MessageResponse } from 'src/app/shared/models/message-response';


@Injectable({
  providedIn: 'root'
})
export class PlantService {
  private readonly requestUrl: string = "https://localhost:5000/gateway/plant";
  private httpOptions = {
    headers: new HttpHeaders({
      "Content-Type": "application/json"
    })
  };

  constructor(private readonly http: HttpClient) { }

  addPlant(plant: Plant): Observable<Plant> {
    return this.http
      .post<MessageResponse>(this.requestUrl, JSON.stringify(plant), this.httpOptions)
      .pipe(
        map((response: MessageResponse) => {
          return response.data as Plant;
        })
      );
  }

  getPlantsByCatalogueId(id: string): Observable<Plant[]> {
    return this.http
      .get<MessageResponse>(`${this.requestUrl}/Catalogue/${id}`, this.httpOptions)
      .pipe(
        map((response: MessageResponse) => {
          return response.data as Plant[];
        })
      );
  }

  getPlantById(id: string): Observable<Plant> {
    return this.http
      .get<MessageResponse>(`${this.requestUrl}/${id}`, this.httpOptions)
      .pipe(
        map((response: MessageResponse) => {
          return response.data as Plant;
        })
      );
  }

  editPlant(plant: Plant): Observable<MessageResponse> {
    return this.http
      .put<MessageResponse>(`${this.requestUrl}`, JSON.stringify(plant), this.httpOptions);
  }

  deletePlant(plant: Plant): Observable<MessageResponse> {
    return this.http
      .delete<MessageResponse>(`${this.requestUrl}`, {
        headers: this.httpOptions.headers,
        body: JSON.stringify(plant)
      });
  }
}
