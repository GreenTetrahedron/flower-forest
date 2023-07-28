import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common'

import { Component, Input, OnInit } from '@angular/core';

import { User } from '../../models/user';

import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  @Input({ required: true }) userId?: string;

  user?: User;

  constructor(private readonly userService: UserService) { }

  ngOnInit(): void {
    this.getUser();
  }

  getUser() {
    if (this.userId == undefined) {
      return;
    }
    this.userService.getUserDetailsById(this.userId)
      .subscribe({
        next: (u: User) => {
          this.user = u;
          this.userId = this.user!.id;
          localStorage.setItem("user", JSON.stringify(this.user));
        },
        error: () => {
        }
      });
  }
}
