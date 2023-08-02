import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { MessageResponse } from 'src/app/shared/models/message-response';
import { User } from '../../models/user';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  @Input() onSubmitDo?: () => void;
  @Input({ required: true }) userId!: string;

  user?: User;

  editUserForm: FormGroup = new FormGroup({
    username: new FormControl(),
    password: new FormControl()
  });

  buttonText: string = "Save";

  validCredentials: boolean = true;

  constructor(private readonly userService: UserService) { }

  ngOnInit(): void {
      this.onInit();
  }

  onInit() {
    this.getUser();
  }

  getUser() {
    this.userService.getUserDetailsById(this.userId)
      .subscribe({
        next: (user: User) => {
          this.user = user;
          this.editUserForm.setControl("username", new FormControl(this.user.username));
        }
      });
  }

  submit() {
    var username: string = this.editUserForm.get("username")?.value;
    var password: string = this.editUserForm.get("password")?.value;

    this.userService.editUser(this.userId, username, password)
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
