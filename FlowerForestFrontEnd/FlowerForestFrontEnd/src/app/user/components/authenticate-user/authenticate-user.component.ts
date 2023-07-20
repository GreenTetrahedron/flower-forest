import { Component } from '@angular/core';

import { FormControl, FormGroup } from '@angular/forms';

import { UserService } from '../../services/user.service';

import { Router } from '@angular/router';
import { User } from '../../models/user';


@Component({
  selector: 'app-authenticate-user',
  templateUrl: './authenticate-user.component.html',
  styleUrls: ['./authenticate-user.component.css']
})
export class AuthenticateUserComponent {
  constructor(private userService: UserService,
    private router: Router) { }

  authenticationForm: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  submit() {
    var username = this.authenticationForm.get("username")?.value;
    var password = this.authenticationForm.get("password")?.value;

    this.userService.authenticateUser(username, password)
      .subscribe((u: User) => {
        this.router.navigateByUrl(`user/details/${u?.id}`);
      });
  }
}
