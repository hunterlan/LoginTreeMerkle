import { TestBed } from '@angular/core/testing';

import { SharedSnackBarService } from './shared-snack-bar.service';

describe('SharedSnackBarService', () => {
  let service: SharedSnackBarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SharedSnackBarService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
