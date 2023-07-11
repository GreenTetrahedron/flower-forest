import { Component } from '@angular/core';

import { FormControl } from '@angular/forms';


@Component({
  selector: 'app-authenticate-user',
  templateUrl: './authenticate-user.component.html',
  styleUrls: ['./authenticate-user.component.css']
})
export class AuthenticateUserComponent {
  username = new FormControl("");
  password = new FormControl("");

  authenticate() {
    
  }
}
