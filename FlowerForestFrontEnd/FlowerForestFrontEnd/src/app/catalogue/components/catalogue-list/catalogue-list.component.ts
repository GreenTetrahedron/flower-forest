import { Component, Input, OnInit } from '@angular/core';

import { CatalogueService } from '../../services/catalogue.service';
import { Catalogue } from '../../models/catalogue';

@Component({
  selector: 'app-catalogue-list',
  templateUrl: './catalogue-list.component.html',
  styleUrls: ['./catalogue-list.component.css']
})
export class CatalogueListComponent {
  @Input({ required: true }) catalogues!: Catalogue[];
  @Input() onViewDetailsDo?: (catalogueId: string) => void;

  constructor(private catalogueService: CatalogueService) { };
}
