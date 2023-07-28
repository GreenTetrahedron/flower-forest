import { Component, Input } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { PlantService } from '../../services/plant.service';
import { Plant } from '../../models/plant';
import { Catalogue } from 'src/app/catalogue/models/catalogue';

@Component({
  selector: 'app-add-plant',
  templateUrl: './add-plant.component.html',
  styleUrls: ['./add-plant.component.css']
})
export class AddPlantComponent {
  @Input({ required: true }) catalogueId!: string;
  @Input() onSubmitDo?: () => void;

  plant?: Plant;

  buttonText: string = "Add";


  addPlantFormGroup: FormGroup = new FormGroup({
    genus: new FormControl(),
    species: new FormControl(),
    commonName: new FormControl(),
    photoUrl: new FormControl(),
    maxHeight_metres: new FormControl()
  });

  constructor(private readonly plantService: PlantService) { }

  onInit() {
    this.buttonText = "Add";
    this.plant = undefined;
  }

  submit() {
    this.plant = {
      catalogueId: this.catalogueId,
      genus: this.addPlantFormGroup.get("genus")?.value,
      species: this.addPlantFormGroup.get("species")?.value,
      commonName: this.addPlantFormGroup.get("commonName")?.value,
      photoUrl: this.addPlantFormGroup.get("photoUrl")?.value,
      maxHeight_metres: Number(this.addPlantFormGroup.get("maxHeight_metres")?.value),
    } as Plant;

    this.plantService.addPlant(this.plant)
      .subscribe({
        next: (plant: Plant) => {
          if (plant != undefined) {
            this.plant = plant;
            this.buttonText = "Saved";
            if (this.onSubmitDo != undefined) {
              this.onSubmitDo();
            }
          }
        }
      });
  }
}
