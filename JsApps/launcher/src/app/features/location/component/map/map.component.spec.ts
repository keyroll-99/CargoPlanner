import {ComponentFixture, TestBed} from '@angular/core/testing';

import {MapComponent} from './map.component';
import {LeafletModule} from "@asymmetrik/ngx-leaflet";

describe('MapComponent', () => {
  let component: MapComponent;
  let fixture: ComponentFixture<MapComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MapComponent],
      imports: [LeafletModule]
    });
    fixture = TestBed.createComponent(MapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
