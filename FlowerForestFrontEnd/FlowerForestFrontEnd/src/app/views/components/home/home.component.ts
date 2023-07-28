import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Catalogue } from 'src/app/catalogue/models/catalogue';
import { CatalogueService } from 'src/app/catalogue/services/catalogue.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  catalogues?: Catalogue[];

  constructor(private readonly catalogueService: CatalogueService,
    private readonly router: Router) { }

  ngOnInit(): void {
    this.getCatalogues()
  }

  getCatalogues() {
    this.catalogueService.getPublicCatalogues()
      .subscribe({
        next: (catalogues: Catalogue[]) => {
          this.catalogues = catalogues;
        }
      });
  }

  onViewingCatalogue(catalogueId: string) {
    this.router.navigateByUrl(`catalogue/${catalogueId}`);
  }
}
