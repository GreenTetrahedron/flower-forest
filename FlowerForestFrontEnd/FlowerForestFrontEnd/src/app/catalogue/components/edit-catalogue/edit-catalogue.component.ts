import { Component, Input, OnInit } from '@angular/core';

import { FormControl, FormGroup } from '@angular/forms';


import { CatalogueService } from '../../services/catalogue.service';

import { Catalogue } from '../../models/catalogue';
import { MessageResponse } from 'src/app/shared/models/message-response';

@Component({
  selector: 'app-edit-catalogue',
  templateUrl: './edit-catalogue.component.html',
  styleUrls: ['./edit-catalogue.component.css']
})
export class EditCatalogueComponent implements OnInit {
  @Input() onSubmitDo?: () => void;

  @Input({ required: true }) catalogueId!: string;
  catalogue?: Catalogue;

  buttonText: string = "Save";

  privacyOptions: string[] = [
    "Public",
    "Private"
  ];

  editCatalogueFormGroup: FormGroup = new FormGroup({
    "name": new FormControl(),
    "privacyOption": new FormControl()
  });


  constructor(private readonly catalogueService: CatalogueService) { }

  ngOnInit(): void {
    this.onInit();
  }


  onInit() {
    this.catalogue = undefined;
    this.getCatalogue();

    this.buttonText = "Save";
  }

  getCatalogue() {
    this.catalogueService.getCatalogueById(this.catalogueId)
      .subscribe({
        next: (catalogue: Catalogue) => {
          this.catalogue = catalogue;
          this.editCatalogueFormGroup.setControl("name", new FormControl(this.catalogue.name));
          this.editCatalogueFormGroup.setControl("privacyOption", new FormControl(this.getPrivacyOptionByIsPublic(this.catalogue.isPublic)));
        }
      });
  }

  getPrivacyOptionByIsPublic(isPublic: boolean) {
    return isPublic == true ? 
      "Public" : "Private";
  }

  getIsPublicByPrivacyOption(privacyOption: string) {
    return privacyOption == "Public";
  }

  onSubmit() {
    if (this.catalogue == undefined) {
      return;
    }

    const isPublic = this.getIsPublicByPrivacyOption(String(this.editCatalogueFormGroup.get("privacyOption")?.value));
    const name = String(this.editCatalogueFormGroup.get("name")?.value);
    const userId = this.catalogue.userId;

    this.catalogue = {
      id: this.catalogueId,
      userId: userId,
      name: name,
      isPublic: isPublic
    } as Catalogue;

    console.log(JSON.stringify(this.catalogue));

    this.catalogueService.editCatalogue(this.catalogue)
      .subscribe((response: MessageResponse) => {
        if ((response.message.startsWith("SUCCESS")) == true) {
          this.buttonText = "Saved";
          if (this.onSubmitDo != undefined) {
            this.onSubmitDo();
          }
        }
      });
  }
}
