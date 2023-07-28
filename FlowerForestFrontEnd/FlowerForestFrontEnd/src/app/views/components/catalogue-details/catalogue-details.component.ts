import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Catalogue } from 'src/app/catalogue/models/catalogue';
import { CatalogueService } from 'src/app/catalogue/services/catalogue.service';
import { Plant } from 'src/app/plant/models/plant';
import { PlantService } from 'src/app/plant/services/plant.service';

@Component({
  selector: 'app-catalogue-details',
  templateUrl: './catalogue-details.component.html',
  styleUrls: ['./catalogue-details.component.css']
})
export class CatalogueDetailsComponent implements OnInit {
  catalogue?: Catalogue;
  plants?: Plant[];

  selectedPlant?: Plant;

  viewingPlant = false;

  constructor(private readonly route: ActivatedRoute,
    private readonly catalogueService: CatalogueService, private readonly plantService: PlantService) { }


  ngOnInit(): void {
    this.onInit()
  }

  onInit() {
    this.getCatalogue()
      .subscribe({
        next: (catalogue: Catalogue) => {
          this.getPlants(catalogue.id);
        }
      })
  }


  onViewingPlant(plantId: string) {
    this.plantService.getPlantById(plantId)
      .subscribe({
        next: (plant: Plant) => {
          this.viewingPlant = true;
          this.selectedPlant = plant;
        }
      });
  }


  onClosePlantDetails() {
    this.viewingPlant = false;
  }

  getCatalogue(): Observable<Catalogue> {
    const id = String(this.route.snapshot.paramMap.get("id"));

    var response = this.catalogueService.getCatalogueById(id)

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
}
