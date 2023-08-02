import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { EditCatalogueComponent } from 'src/app/catalogue/components/edit-catalogue/edit-catalogue.component';
import { Catalogue } from 'src/app/catalogue/models/catalogue';
import { CatalogueService } from 'src/app/catalogue/services/catalogue.service';
import { AddPlantComponent } from 'src/app/plant/components/add-plant/add-plant.component';
import { EditPlantComponent } from 'src/app/plant/components/edit-plant/edit-plant.component';
import { PlantListComponent } from 'src/app/plant/components/plant-list/plant-list.component';
import { Plant } from 'src/app/plant/models/plant';
import { PlantService } from 'src/app/plant/services/plant.service';
import { UserStorageService } from 'src/app/storage/services/user-storage/user-storage.service';
import { User } from 'src/app/user/models/user';
import { UserService } from 'src/app/user/services/user.service';

@Component({
  selector: 'app-catalogue-dashboard',
  templateUrl: './catalogue-dashboard.component.html',
  styleUrls: ['./catalogue-dashboard.component.css']
})
export class CatalogueDashboardComponent {
  user?: User;

  catalogueId?: string;
  catalogue?: Catalogue;

  plants?: Plant[];

  selectedPlant?: Plant;

  addingPlant: boolean = false;
  editingPlant: boolean = false;

  authorized: boolean = false;

  @ViewChild("plantListComponent", { static: false }) plantListComponent?: PlantListComponent;
  @ViewChild("addPlantComponent", { static: false }) addPlantComponent?: AddPlantComponent;
  @ViewChild("editPlantComponent", { static: false }) editPlantComponent?: EditPlantComponent;
  @ViewChild("editCatalogueComponent", { static: true }) editCatalogueComponent?: EditCatalogueComponent;


  constructor(private route: ActivatedRoute, private router: Router,
    private readonly catalogueService: CatalogueService, private readonly plantService: PlantService,
    private readonly userService: UserService) { }

  ngOnInit() {
    this.getCatalogue()
      .subscribe({
        next: (catalogue: Catalogue) => {
          this.getUser(catalogue.userId);
        }
      });

    if (this.catalogueId != undefined) {
      this.getPlants(this.catalogueId);
    }
  }

  getUser(userId: string) {
    this.userService.getUserDetailsById(userId)
      .subscribe({
        next: (user: User) => {
          this.user = user;
        }
      });
  }

  getCatalogue(): Observable<Catalogue> {
    this.catalogueId = String(this.route.snapshot.paramMap.get("catalogueId"));

    var response = this.catalogueService.getCatalogueById(this.catalogueId)
    response.subscribe({
        next: (catalogue: Catalogue) => {
          this.catalogue = catalogue;
        }
      });
    
    return response;
  }

  getPlants(catalogueId: string) {
    this.plantService.getPlantsByCatalogueId(catalogueId)
      .subscribe({
        next: (plants: Plant[]) => {
          this.plants = plants;
        }
      });
  }


  onAddingPlant() {
    if (this.addPlantComponent == undefined || this.plantListComponent == undefined
      || this.addPlantComponent.plant == undefined || this.plantListComponent.plants == undefined) {
        return;
    }

    this.plantListComponent.plants.push(this.addPlantComponent.plant);
    this.addPlantComponent.onInit();

    this.addingPlant = false;
  }

  onViewingPlant(plantId: string) {
    this.plantService.getPlantById(plantId)
      .subscribe({
        next: (plant: Plant) => {
          this.selectedPlant = plant;
          this.addingPlant = false;
          this.editingPlant = true;
          
          if (this.editPlantComponent == undefined) {
            return;
          }

          this.editPlantComponent.plantId = this.selectedPlant.id;
          this.editPlantComponent?.onInit();
        }
      });
  }

  onEditingPlant() {
    if (this.editPlantComponent == undefined || this.plantListComponent == undefined
      || this.editPlantComponent.plant == undefined || this.plantListComponent.plants == undefined) {
      return;
    }

    this.plantListComponent.plants
      .map((plant: Plant) => {
        console.log(plant);
        if (plant.id == this.editPlantComponent?.plantId) {
          return this.editPlantComponent.plant;
        }
        return plant;
      });
    
    this.editingPlant = false;
  }

  onDeletingPlant() {
    const url = this.router.url;
    this.router.navigateByUrl("/", { skipLocationChange: true })
      .then(() => this.router.navigateByUrl(url));

    this.editingPlant = false;
  }

  onEditingCatalogue() {
    if (this.editCatalogueComponent == undefined) {
      console.log(this.editCatalogueComponent);
      return;
    }

    this.catalogue = this.editCatalogueComponent!.catalogue;
    this.editCatalogueComponent!.onInit();
  }


  displayAddPlantForm() {
    this.addingPlant = true;
    this.editingPlant = false;
  }

  hideAddPlant() {
    this.addingPlant = false;
  }

  displayEditPlantForm() {
    this.editingPlant = true;
    this.addingPlant = false;
  }
  
  hideEditPlant() {
    this.editingPlant = false;
  }
}
