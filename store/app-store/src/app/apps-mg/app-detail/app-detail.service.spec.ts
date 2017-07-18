import { TestBed, inject } from '@angular/core/testing';

import { AppDetailService } from './app-detail.service';

describe('AppDetailService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AppDetailService]
    });
  });

  it('should ...', inject([AppDetailService], (service: AppDetailService) => {
    expect(service).toBeTruthy();
  }));
});
