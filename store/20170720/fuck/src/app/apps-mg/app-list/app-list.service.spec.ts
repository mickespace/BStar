import { TestBed, inject } from '@angular/core/testing';

import { AppListService } from './app-list.service';

describe('AppListService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AppListService]
    });
  });

  it('should ...', inject([AppListService], (service: AppListService) => {
    expect(service).toBeTruthy();
  }));
});
