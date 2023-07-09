import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MessageResponse } from 'src/app/shared/models/response';

@Injectable({
  providedIn: 'root'
})
export class PlantService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  private readonly requestPath: string = "https://localhost:44375/api/Plant/";

  constructor(private http: HttpClient) { }

  getPlants(): Observable<MessageResponse> {
    return this.http.get<MessageResponse>(this.requestPath, this.httpOptions);
  }

  getPlantById(id: string): Observable<MessageResponse> {
    return this.http.get<MessageResponse>(this.requestPath + id, this.httpOptions);
  }
}
