import {TestBed} from '@angular/core/testing';
import {Router} from '@angular/router';
import {authGuard} from './auth.guard';
import {AuthService} from '../services/auth.service';
import {lastValueFrom, of} from 'rxjs';

describe('authGuard', () => {
  const mockRouter = jasmine.createSpyObj<Router>(['navigate']);
  mockRouter.navigate.and.returnValue(lastValueFrom(of(true)));

  const mockAuthService = jasmine.createSpyObj<AuthService>(["isAuthenticated"])


  const setup = () => {
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
    mockAuthService.isAuthenticated.and.returnValue(true)

    const guard = setup();

    expect(guard).toBeTrue();
  })

  it("should redirect to login page", () => {
    mockAuthService.isAuthenticated.and.returnValue(false)


    const guard = setup();

    expect(guard).toBeFalse();
    expect(mockRouter.navigate).toHaveBeenCalled();
  })

});
