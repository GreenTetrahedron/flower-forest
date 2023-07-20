import { TestBed } from '@angular/core/testing';

import { ApiInteractionsService } from './api-interactions.service';

describe('ApiInteractionsService', () => {
  let service: ApiInteractionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiInteractionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
