import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user';
import { MessageResponse } from 'src/app/shared/models/message-response';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent {
  addUserForm: FormGroup = new FormGroup({
    username: new FormControl(),
    password: new FormControl()
  });

  validCredentials: boolean = true;

  constructor(private readonly userService: UserService,
    private readonly router: Router) { }

  submit() {
    var username: string = this.addUserForm.get("username")?.value;
    var password: string = this.addUserForm.get("password")?.value;

    this.userService.addUser(username, password)
      .subscribe({
        next: (response: MessageResponse) => {
          if (response.message.startsWith("SUCCESS")) {
            const user: User = response.data as User;

            this.router.navigateByUrl(`user/${user.id}`);
          }
          else {
            this.validCredentials = false;
          }
        },
        error: (error) => {
          console.log(error);
        }
      });
  }
}
