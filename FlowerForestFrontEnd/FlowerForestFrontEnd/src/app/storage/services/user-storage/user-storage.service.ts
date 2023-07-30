import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { User } from 'src/app/user/models/user';
import { StorageItems } from '../../storage-keys';
import { StorageService } from '../storage.service';

@Injectable({
  providedIn: 'root'
})
export class UserStorageService {

  userChanges: Subject<User | undefined> = new Subject<User | undefined>();

  constructor(private readonly storageService: StorageService) {
  }

  getUser() {
    return this.storageService.getItem<User>(StorageItems.USER);
  }

  setUser(user: User) {
    this.storageService.storeItem(StorageItems.USER, user);
    this.userChanges.next(user);
  }

  clearUser() {
    this.storageService.removeItem(StorageItems.USER);
    this.userChanges.next(undefined);
  }
}
