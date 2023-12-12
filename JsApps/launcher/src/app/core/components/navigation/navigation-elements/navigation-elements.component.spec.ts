import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavigationElementsComponent } from './navigation-elements.component';

describe('NavigationElementsComponent', () => {
  let component: NavigationElementsComponent;
  let fixture: ComponentFixture<NavigationElementsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NavigationElementsComponent]
    });
    fixture = TestBed.createComponent(NavigationElementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  xit('should create', () => {
    expect(component).toBeTruthy();
  });
});
