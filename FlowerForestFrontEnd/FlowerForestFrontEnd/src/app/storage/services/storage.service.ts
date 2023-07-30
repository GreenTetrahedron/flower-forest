import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { StorageItems } from '../storage-keys';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  storageChanges = new Subject<any[]>();

  storage: {key: string, value: any}[] = [];

  constructor() { }

  getItem<T>(item: StorageItems): T {
    return JSON.parse(String(localStorage.getItem(item.toString()))) as T;
  }

  storeItem(item: StorageItems, value: any) {
    localStorage.setItem(item.toString(), JSON.stringify(value));
  }

  removeItem(item: StorageItems) {
    localStorage.removeItem(item.toString());
  }
}
