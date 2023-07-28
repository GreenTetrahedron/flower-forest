import { Component, Input } from '@angular/core';
import { Plant } from '../../models/plant';

@Component({
  selector: 'app-plant-details',
  templateUrl: './plant-details.component.html',
  styleUrls: ['./plant-details.component.css']
})
export class PlantDetailsComponent {
  @Input() plant?: Plant;

}
