import { Component } from '@angular/core';

import { FormControl, FormGroup } from '@angular/forms';

import { UserService } from '../../services/user.service';
import { UserDTOWithToken } from '../../models/userDTOWithToken';

import { Router } from '@angular/router';


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
    console.log("HELLO");

    var username = this.authenticationForm.get("username")?.value;
    var password = this.authenticationForm.get("password")?.value;

    this.userService.authenticateUser(username, password)
      .subscribe(r => this.router.navigateByUrl(`user/details/${(r.data as UserDTOWithToken).user.id}`));
  }
}
