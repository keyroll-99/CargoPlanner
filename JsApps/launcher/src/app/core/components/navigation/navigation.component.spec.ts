import {ComponentFixture, TestBed} from '@angular/core/testing';

import {NavigationComponent} from './navigation.component';
import {AuthService} from "../../services/auth.service";
import {Router, RouterEvent} from "@angular/router";
import {of, ReplaySubject} from "rxjs";

describe('NavigationComponent', () => {
  let component: NavigationComponent;
  let fixture: ComponentFixture<NavigationComponent>;

  const eventSubject = new ReplaySubject<RouterEvent>()

  const mockAuthService = jasmine.createSpyObj<AuthService>(["updateUserModel", "isAuthenticated"]);
  const mockRouter = {
    navigate: jasmine.createSpy("navigate"),
    events: eventSubject
  }


  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NavigationComponent],
      providers: [
        {
          provide: AuthService, useValue: mockAuthService
        },
        {
          provide: Router, useValue: mockRouter
        }
      ]
    });
    fixture = TestBed.createComponent(NavigationComponent);


    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    mockAuthService.updateUserModel.and.returnValue(of({permission: 1, email: "test", id: "aasd", isActive: true}))
    mockAuthService.isAuthenticated.and.returnValue(false)

    expect(component).toBeTruthy();
  });
});
