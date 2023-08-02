import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';


import { User } from 'src/app/user/models/user';
import { Catalogue } from 'src/app/catalogue/models/catalogue';
import { ActivatedRoute, Router } from '@angular/router';
import { CatalogueService } from 'src/app/catalogue/services/catalogue.service';
import { AddCatalogueComponent } from 'src/app/catalogue/components/add-catalogue/add-catalogue.component';
import { CatalogueListComponent } from 'src/app/catalogue/components/catalogue-list/catalogue-list.component';
import { EditCatalogueComponent } from 'src/app/catalogue/components/edit-catalogue/edit-catalogue.component';


@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {
  user?: User;
  userId?: string;

  catalogues: Catalogue[] = [];

  selectedCatalogue?: Catalogue;

  addingCatalogue: boolean = false;

  @ViewChild("addCatalogueComponent", { static: false }) addCatalogueComponent?: AddCatalogueComponent;
  @ViewChild("catalogueListComponent", { static: true }) catalogueListComponent?: CatalogueListComponent;


  constructor(private readonly route: ActivatedRoute, private readonly router: Router,
    private readonly catalogueService: CatalogueService) { }

  ngOnInit(): void {
    this.getUserIdAndUser();

    if (this.userId != undefined)
      this.getCatalogues(this.userId);
  }


  onAddingCatalogue() {
    if (this.catalogueListComponent == undefined || this.addCatalogueComponent == undefined ||
      this.catalogueListComponent.catalogues == undefined || this.addCatalogueComponent.catalogue == undefined) {
      return;
    }

    this.catalogueListComponent.catalogues?.push(this.addCatalogueComponent.catalogue!);
    this.hideAddCatalogue();
  }

  onViewingCatalogueDetails(catalogueId: string) {
    this.catalogueService.getCatalogueById(catalogueId)
      .subscribe({
        next: (catalogue: Catalogue) => {
          this.selectedCatalogue = catalogue;
          this.router.navigateByUrl(`/user/${this.userId}/catalogue/${this.selectedCatalogue.id}`)
        }
      });
    
  }


  getUserIdAndUser() {
    this.userId = String(this.route.snapshot.paramMap.get("id"));
    this.user = JSON.parse(String(localStorage.getItem("user"))) as User;
  }

  getCatalogues(id: string) {
    this.catalogueService.getCataloguesByUserId(id)
      .subscribe({
        next: (response: Catalogue[]) => this.catalogues = response
      });
  }


  displayAddCatalogueForm() {
    this.addingCatalogue = true;
  }
  hideAddCatalogue() {
    this.addingCatalogue = false;
  }
}
