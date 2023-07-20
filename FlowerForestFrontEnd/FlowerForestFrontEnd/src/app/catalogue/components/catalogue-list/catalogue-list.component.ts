import { Component, OnInit } from '@angular/core';

import { CatalogueService } from '../../services/catalogue.service';
import { Catalogue } from '../../models/catalogue';

@Component({
  selector: 'app-catalogue-list',
  templateUrl: './catalogue-list.component.html',
  styleUrls: ['./catalogue-list.component.css']
})
export class CatalogueListComponent implements OnInit{
  catalogues: Catalogue[] = [];

  constructor(private catalogueService: CatalogueService) { };

  ngOnInit(): void {
      this.getCatalogues()
  }

  getCatalogues() {
    this.catalogues = this.catalogueService.getCatalogues();
  }
}
