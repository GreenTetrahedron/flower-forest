import { Component } from '@angular/core';

import { FormControl, FormGroup } from '@angular/forms';

import { UserService } from '../../services/user.service';

import { Router } from '@angular/router';
import { AuthenticationResult } from '../../models/authenticationResult';
import { UserStorageService } from 'src/app/storage/services/user-storage/user-storage.service';


@Component({
  selector: 'app-authenticate-user',
  templateUrl: './authenticate-user.component.html',
  styleUrls: ['./authenticate-user.component.css']
})
export class AuthenticateUserComponent {
  constructor(private readonly userService: UserService,
    private router: Router) { }

  authenticationForm: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  validCredentials: boolean = true;

  submit() {
    var username = this.authenticationForm.get("username")?.value;
    var password = this.authenticationForm.get("password")?.value;

    this.userService.authenticateUser(username, password)
      .subscribe({
        next: (result: AuthenticationResult) => {
          if (result.authenticationSuccess === true) {
            this.router.navigateByUrl(`user/${result.user.id}`);
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
