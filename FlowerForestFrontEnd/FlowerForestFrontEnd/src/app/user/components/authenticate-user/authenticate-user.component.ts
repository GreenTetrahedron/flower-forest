import { Component } from '@angular/core';

import { FormControl, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-authenticate-user',
  templateUrl: './authenticate-user.component.html',
  styleUrls: ['./authenticate-user.component.css']
})
export class AuthenticateUserComponent {

  authenticationForm: FormGroup = new FormGroup ({
    username: new FormControl(''),
    password: new FormControl(''),
  });

  submit() {
    
  }
}
