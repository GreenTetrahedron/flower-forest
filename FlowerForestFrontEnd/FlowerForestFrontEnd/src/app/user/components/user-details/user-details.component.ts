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
export class UserDetailsComponent implements OnInit{
  @Input() user?: User;

  constructor(private readonly userService: UserService,
    private readonly route: ActivatedRoute, private readonly location: Location) { }

  ngOnInit(): void {
      this.getUser();
  }

  getUser() {
    const id = String(this.route.snapshot.paramMap.get("id"));

    this.userService.getUserDetailsById(id)
      .subscribe((u: User) => {
        this.user = u;
      });
  }
}
