import {TestBed} from '@angular/core/testing';

import {LocationSearchService} from './location-search.service';
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('LocationSearchService', () => {
  let service: LocationSearchService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(LocationSearchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
