import {TestBed} from '@angular/core/testing';

import {AuthInterceptor} from './auth.interceptor';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {Router} from "@angular/router";
import {AuthService} from "../services/auth.service";

describe('AuthInterceptor', () => {
  const mockRouter = jasmine.createSpyObj<Router>(["navigate"])
  const mockAuthService = jasmine.createSpyObj<AuthService>(["setAuth"])

  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      AuthInterceptor,
      {
        provide: AuthService, useValue: mockAuthService
      },
      {
        provide: Router, useValue: mockRouter
      }
    ],
    imports: [HttpClientTestingModule],
  }));

  it('should be created', () => {
    const interceptor: AuthInterceptor = TestBed.inject(AuthInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
