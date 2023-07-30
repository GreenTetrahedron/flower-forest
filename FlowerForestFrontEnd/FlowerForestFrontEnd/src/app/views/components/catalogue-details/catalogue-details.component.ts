import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Catalogue } from 'src/app/catalogue/models/catalogue';
import { CatalogueService } from 'src/app/catalogue/services/catalogue.service';
import { Plant } from 'src/app/plant/models/plant';
import { PlantService } from 'src/app/plant/services/plant.service';
import { User } from 'src/app/user/models/user';
import { UserService } from 'src/app/user/services/user.service';

@Component({
  selector: 'app-catalogue-details',
  templateUrl: './catalogue-details.component.html',
  styleUrls: ['./catalogue-details.component.css']
})
export class CatalogueDetailsComponent implements OnInit {
  userId?: string;
  user?: User;

  catalogue?: Catalogue;
  plants?: Plant[];

  selectedPlant?: Plant;

  viewingPlant = false;

  constructor(private readonly route: ActivatedRoute,
    private readonly catalogueService: CatalogueService, private readonly plantService: PlantService, private readonly userService: UserService) { }


  ngOnInit(): void {
    this.onInit()
  }

  onInit() {
    this.getCatalogue()
      .subscribe({
        next: (catalogue: Catalogue) => {
          this.getPlants(catalogue.id);
          this.userId = catalogue.userId;
          this.getUser(this.userId);
        }
    });
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

  getUser(userId: string) {
    this.userService.getUserDetailsById(userId)
      .subscribe({
        next: (user: User) => {
          this.user = user;
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
}
