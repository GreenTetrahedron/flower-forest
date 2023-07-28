import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Catalogue } from 'src/app/catalogue/models/catalogue';
import { CatalogueService } from 'src/app/catalogue/services/catalogue.service';
import { AddPlantComponent } from 'src/app/plant/components/add-plant/add-plant.component';
import { EditPlantComponent } from 'src/app/plant/components/edit-plant/edit-plant.component';
import { PlantListComponent } from 'src/app/plant/components/plant-list/plant-list.component';
import { Plant } from 'src/app/plant/models/plant';
import { PlantService } from 'src/app/plant/services/plant.service';

@Component({
  selector: 'app-catalogue-dashboard',
  templateUrl: './catalogue-dashboard.component.html',
  styleUrls: ['./catalogue-dashboard.component.css']
})
export class CatalogueDashboardComponent {
  catalogueId?: string;
  catalogue?: Catalogue;

  plants?: Plant[];

  selectedPlant?: Plant;

  addingPlant: boolean = false;
  editingPlant: boolean = false;

  @ViewChild("plantListComponent", { static: false }) plantListComponent?: PlantListComponent;
  @ViewChild("addPlantComponent", { static: false }) addPlantComponent?: AddPlantComponent;
  @ViewChild("editPlantComponent", { static: false }) editPlantComponent?: EditPlantComponent;


  constructor(private route: ActivatedRoute, private router: Router,
    private readonly catalogueService: CatalogueService, private readonly plantService: PlantService) { }

  ngOnInit() {
    this.getCatalogue();

    if (this.catalogueId != undefined) {
      this.getPlants(this.catalogueId);
    }
  }

  getCatalogue() {
    this.catalogueId = String(this.route.snapshot.paramMap.get("catalogueId"));

    this.catalogueService.getCatalogueById(this.catalogueId)
      .subscribe({
        next: (catalogue: Catalogue) => {
          this.catalogue = catalogue;
        }
      });
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

  displayAddPlantForm() {
    this.addingPlant = true;
    this.editingPlant = false;
  }

  displayEditPlantForm() {
    this.editingPlant = true;
    this.addingPlant = false;
  }
}
