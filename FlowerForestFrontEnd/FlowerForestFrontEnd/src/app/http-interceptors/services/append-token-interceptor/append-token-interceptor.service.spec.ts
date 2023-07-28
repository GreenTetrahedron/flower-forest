import { TestBed } from '@angular/core/testing';

import { AppendTokenInterceptorService } from './append-token-interceptor.service';

describe('AppendTokenInterceptorService', () => {
  let service: AppendTokenInterceptorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppendTokenInterceptorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
