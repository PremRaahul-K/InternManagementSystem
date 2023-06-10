import { TestBed } from '@angular/core/testing';

import { InternRegisterService } from './intern-register-service';

describe('InternRegisterServiceService', () => {
  let service: InternRegisterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InternRegisterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
