import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common'

import { Component, Input, OnInit } from '@angular/core';
import { Plant } from '../../models/plant';

import { PlantService } from '../../services/plant.service';

@Component({
  selector: 'app-plant-details',
  templateUrl: './plant-details.component.html',
  styleUrls: ['./plant-details.component.css']
})
export class PlantDetailsComponent implements OnInit {
  @Input() plant?: Plant;

  constructor(private plantService: PlantService,
    private route: ActivatedRoute, private location: Location)
  { }

  ngOnInit() {
    this.getPlant();
  }

  getPlant() {
    const id = String(this.route.snapshot.paramMap.get("id"));
    this.plantService.getPlantById(id)
      .subscribe(r => this.plant = r.data);
  }

  goBack() {
    this.location.back();
  }
}
