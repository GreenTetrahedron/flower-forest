import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { StorageService } from '../storage.service';
import { StorageItems } from '../../storage-keys';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {

  tokenChanges: Subject<string | undefined> = new Subject<string | undefined>();

  constructor(private readonly storageService: StorageService) { }

  getToken() {
    return this.storageService.getItem<string>(StorageItems.TOKEN);
  }

  setToken(token: string) {
    this.storageService.storeItem(StorageItems.TOKEN, token);
    this.tokenChanges.next(token);
  }

  clearToken() {
    this.storageService.removeItem(StorageItems.TOKEN);
    this.tokenChanges.next(undefined);
  }
}
