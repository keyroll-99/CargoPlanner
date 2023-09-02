import {ComponentFixture, TestBed} from '@angular/core/testing';

import {LocationSearchComponent} from './location-search.component';
import {LocationSearchService} from "../../services/location-search.service";

describe('LocationSearchComponent', () => {
  let component: LocationSearchComponent;
  let fixture: ComponentFixture<LocationSearchComponent>;

  const mockLocationSearchService = jasmine.createSpyObj<LocationSearchService>(["search"]);

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LocationSearchComponent],
      providers: [
        {
          provide: LocationSearchService, useValue: mockLocationSearchService
        }
      ]
    });
    fixture = TestBed.createComponent(LocationSearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
