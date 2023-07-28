import { TestBed } from '@angular/core/testing';

import { CatchErrorInterceptorService } from './catch-error-interceptor.service';

describe('CatchErrorInterceptorService', () => {
  let service: CatchErrorInterceptorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CatchErrorInterceptorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
