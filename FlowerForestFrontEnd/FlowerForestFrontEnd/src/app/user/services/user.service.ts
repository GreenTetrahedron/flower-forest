import { Injectable } from '@angular/core';

import { ApiInteractionsService } from 'src/app/api-interactions/services/api-interactions.service';

import { UserWithToken } from '../models/userWithToken';
import { User } from '../models/user';
import { MessageResponse } from 'src/app/api-interactions/models/message-response';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly controller: string = "User";

  constructor(private apiInteractionsService: ApiInteractionsService) { }

  authenticateUser(username: string, password: string): Observable<User> {
    var response = this.apiInteractionsService.postToApi(this.controller, { "username": username, "password": password }, "Authenticate")
    
    response.subscribe((r: MessageResponse) => {
      this.apiInteractionsService.setToken((r.data as UserWithToken).token);
    });

    var user = response.pipe(
      map((r: MessageResponse) => (r.data as UserWithToken).user as User)
    );

    return user;
  }

  getUserDetailsById(id: string): Observable<User> {
    var user = this.apiInteractionsService.getFromApi(this.controller, [id]).pipe(
      map((x: MessageResponse) => {
        return x.data as User;
      })
    );

    return user;
  }
}
