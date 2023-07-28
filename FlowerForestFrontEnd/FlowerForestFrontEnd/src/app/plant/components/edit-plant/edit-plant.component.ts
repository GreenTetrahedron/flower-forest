import { Component, Input, OnInit } from '@angular/core';
import { Plant } from '../../models/plant';
import { FormControl, FormGroup } from '@angular/forms';
import { PlantService } from '../../services/plant.service';

@Component({
  selector: 'app-edit-plant',
  templateUrl: './edit-plant.component.html',
  styleUrls: ['./edit-plant.component.css']
})
export class EditPlantComponent implements OnInit{
  @Input({ required: true }) catalogueId!: string;
  @Input({ required: true }) plantId!: string;
  @Input() onSubmitDo?: () => void;
  @Input() onDeleteDo?: () => void;

  plant?: Plant;

  buttonText: string = "Add";
  buttonDisabled: boolean = false;

  editPlantFormGroup: FormGroup = new FormGroup({
    genus: new FormControl(),
    species: new FormControl(),
    commonName: new FormControl(),
    photoUrl: new FormControl(),
    maxHeight_metres: new FormControl()
  });

  constructor(private readonly plantService: PlantService) { }

  ngOnInit() {
    this.onInit();
  }

  onInit() {
    this.buttonText = "Add";
    this.plant = undefined;
    this.getPlant();
  }

  getPlant() {
    this.plantService.getPlantById(this.plantId)
      .subscribe({
        next: (plant: Plant) => {
          this.plant = plant;
          this.editPlantFormGroup.setControl("genus", new FormControl(this.plant.genus));
          this.editPlantFormGroup.setControl("species", new FormControl(this.plant.species));
          this.editPlantFormGroup.setControl("maxHeight_metres", new FormControl(this.plant.maxHeight_metres));
          this.editPlantFormGroup.setControl("photoUrl", new FormControl(this.plant.photoUrl));
          this.editPlantFormGroup.setControl("commonName", new FormControl(this.plant.commonName));
        }
      });
  }

  submit() {
    this.plant = {
      id: this.plantId,
      catalogueId: this.catalogueId,
      genus: this.editPlantFormGroup.get("genus")?.value,
      species: this.editPlantFormGroup.get("species")?.value,
      commonName: this.editPlantFormGroup.get("commonName")?.value,
      photoUrl: this.editPlantFormGroup.get("photoUrl")?.value,
      maxHeight_metres: Number(this.editPlantFormGroup.get("maxHeight_metres")?.value),
    } as Plant;

    this.plantService.editPlant(this.plant)
      .subscribe({
        next: (response) => {
          if (response.message.startsWith("SUCCESS")) {
            this.buttonText = "Saved";
            if (this.onSubmitDo != undefined) {
              this.onSubmitDo();
            }
            this.plant = undefined;
          }
        }
      });
  }

  deletePlant() {
    this.plant = {
      id: this.plantId,
      catalogueId: this.catalogueId,
      genus: this.editPlantFormGroup.get("genus")?.value,
      species: this.editPlantFormGroup.get("species")?.value,
      commonName: this.editPlantFormGroup.get("commonName")?.value,
      photoUrl: this.editPlantFormGroup.get("photoUrl")?.value,
      maxHeight_metres: Number(this.editPlantFormGroup.get("maxHeight_metres")?.value),
    } as Plant;

    this.plantService.deletePlant(this.plant)
      .subscribe({
        next: (response) => {
          if (response.message.startsWith("SUCCESS")) {
            if (this.onDeleteDo != undefined) {
              this.onDeleteDo();
            }
            this.plant = undefined;
          }
        }
      });

  }
}
