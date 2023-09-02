import {TestBed} from '@angular/core/testing';
import {Router} from '@angular/router';

import {loginPageGuard} from './login-page.guard';
import {lastValueFrom, of} from "rxjs";
import {AuthService} from "../services/auth.service";

describe('loginPageGuard', () => {
  const mockRouter = jasmine.createSpyObj<Router>(['navigate']);
  mockRouter.navigate.and.returnValue(lastValueFrom(of(true)));

  const mockAuthService = jasmine.createSpyObj<AuthService>(['isAuthenticated'])

  const setup = () => {
    TestBed.configureTestingModule({
      providers: [
        loginPageGuard,
        {provide: AuthService, useValue: mockAuthService},
        {provide: Router, useValue: mockRouter},
      ]
    })

    // @ts-ignore
    return TestBed.runInInjectionContext(loginPageGuard);
  }

  it("should allow to continue", () => {
    mockAuthService.isAuthenticated.and.returnValue(false)

    const guard = setup();

    expect(guard).toBeTrue();
  })

  it("should redirect to login page", () => {
    mockAuthService.isAuthenticated.and.returnValue(true)


    const guard = setup();

    expect(guard).toBeFalse();
    expect(mockRouter.navigate).toHaveBeenCalled();
  })
});
