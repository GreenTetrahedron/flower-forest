import { Injectable } from '@angular/core';

import { ApiInteractionsService } from 'src/app/api-interactions/services/api-interactions.service';

import { Catalogue } from '../models/catalogue';

@Injectable({
  providedIn: 'root'
})
export class CatalogueService {
  private readonly controller: string = "Catalogue";

  constructor(private apiInteractionsService: ApiInteractionsService) { }

  getCatalogues(): Catalogue[] {
    var catalogues: Catalogue[] = [];

    this.apiInteractionsService.getFromApi(this.controller, undefined, "Public")
      .subscribe(r => catalogues = (r.data as Catalogue[]));
    
    return catalogues;
  }
}
