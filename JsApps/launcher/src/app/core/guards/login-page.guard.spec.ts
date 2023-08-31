import { TestBed } from '@angular/core/testing';
import {CanActivateFn, Router} from '@angular/router';

import { loginPageGuard } from './login-page.guard';
import {lastValueFrom, of} from "rxjs";
import {authGuard} from "./auth.guard";
import {AuthService} from "../services/auth.service";

describe('loginPageGuard', () => {
  const mockRouter = jasmine.createSpyObj<Router>(['navigate']);
  mockRouter.navigate.and.returnValue(lastValueFrom(of(true)));


  const setup = (mockAuthService: unknown) => {
    TestBed.configureTestingModule({
      providers: [
        authGuard,
        {provide: AuthService, useValue: mockAuthService},
        {provide: Router, useValue: mockRouter},
      ]
    })

    // @ts-ignore
    return TestBed.runInInjectionContext(authGuard);
  }

  it("should allow to continue", () => {
    const mockAuthService = {
      isAuthenticated: () => false
    }

    const guard = setup(mockAuthService);

    expect(guard).toBeTrue();
  })

  it("should redirect to login page", () => {
    const mockAuthService = {
      isAuthenticated: () => true
    }

    const guard = setup(mockAuthService);

    expect(guard).toBeFalse();
    expect(mockRouter.navigate).toHaveBeenCalled();
  })
});