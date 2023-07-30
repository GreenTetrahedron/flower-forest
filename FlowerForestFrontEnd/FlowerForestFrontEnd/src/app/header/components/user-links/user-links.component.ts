import { Component, OnInit } from '@angular/core';
import { UserStorageService } from 'src/app/storage/services/user-storage/user-storage.service';
import { User } from 'src/app/user/models/user';

@Component({
  selector: 'app-user-links',
  templateUrl: './user-links.component.html',
  styleUrls: ['./user-links.component.css']
})
export class UserLinksComponent implements OnInit{
  user?: User = this.userStorage.getUser();
  
  signedIn: boolean = this.user != undefined;


  constructor(private readonly userStorage: UserStorageService) {}

  ngOnInit(): void {
    this.getUser();
  }

  getUser() {
    this.userStorage.userChanges
      .subscribe({
        next: (user?: User) => {
          this.user = user;
          this.signedIn = user != undefined;
        }
      })
  }

}
