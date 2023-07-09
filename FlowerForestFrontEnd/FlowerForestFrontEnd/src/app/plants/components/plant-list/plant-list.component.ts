import { Component, OnInit } from '@angular/core';
import { PlantService } from '../../services/plant.service';
import { Plant } from '../../models/plant';

@Component({
  selector: 'app-plant-list',
  templateUrl: './plant-list.component.html',
  styleUrls: ['./plant-list.component.css']
})
export class PlantListComponent implements OnInit {
  plants: Plant[] = []

  constructor(private plantService: PlantService) { }

  ngOnInit() {
    this.getPlants();
  }

  getPlants() {
    this.plantService.getPlants()
      .subscribe(r => this.plants = r.data);
  }
}
