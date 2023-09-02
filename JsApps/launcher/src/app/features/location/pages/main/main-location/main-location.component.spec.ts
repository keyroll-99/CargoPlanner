import {ComponentFixture, TestBed} from '@angular/core/testing';

import {MainLocationComponent} from './main-location.component';
import {MapComponent} from "../../../component/map/map.component";
import {LocationSearchComponent} from "../../../component/location-search/location-search.component";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {LeafletModule} from "@asymmetrik/ngx-leaflet";

describe('MainLocationComponent', () => {
  let component: MainLocationComponent;
  let fixture: ComponentFixture<MainLocationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MainLocationComponent, MapComponent, LocationSearchComponent],
      imports: [HttpClientTestingModule, LeafletModule]
    });
    fixture = TestBed.createComponent(MainLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
