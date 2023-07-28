import { Component, Input } from '@angular/core';

import { FormControl, FormGroup } from '@angular/forms';


import { CatalogueService } from '../../services/catalogue.service';


import { Catalogue } from '../../models/catalogue';
import { MessageResponse } from 'src/app/shared/models/message-response';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-catalogue',
  templateUrl: './add-catalogue.component.html',
  styleUrls: ['./add-catalogue.component.css']
})
export class AddCatalogueComponent {
  @Input() userId?: string;
  @Input() onSubmitDo?: () => void;
  buttonText: string = "Add";

  catalogue?: Catalogue;

  constructor(private readonly catalogueService: CatalogueService) { }

  catalogueDetails: FormGroup = new FormGroup({
    name: new FormControl()
  });

  onInit() {
    this.catalogue = undefined;
  }

  onSubmit() {
    const name = this.catalogueDetails.get("name")?.value;
    this.catalogue = { userId: this.userId!, name: name } as Catalogue;

    this.catalogueService.addCatalogue(this.catalogue)
      .subscribe({
        next: (catalogue: Catalogue) => {
          if (catalogue != undefined) {
            this.catalogue = catalogue;
            this.buttonText = "Saved";
            if (this.onSubmitDo != undefined) {
              this.onSubmitDo();
            };
          }
        }
      });
  }
}
