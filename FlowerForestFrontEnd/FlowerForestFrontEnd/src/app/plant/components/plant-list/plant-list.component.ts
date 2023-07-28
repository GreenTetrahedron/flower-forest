import { Component, Input } from '@angular/core';
import { Plant } from '../../models/plant';

@Component({
  selector: 'app-plant-list',
  templateUrl: './plant-list.component.html',
  styleUrls: ['./plant-list.component.css']
})
export class PlantListComponent {
  @Input({ required: true }) plants?: Plant[];
  @Input() onViewDo?: (plantId: string) => void;
}
