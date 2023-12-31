import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import {AuthService} from "../services/auth.service";

export const loginPageGuard: CanActivateFn = () => {
  const authService = inject(AuthService)
  const router = inject(Router)

  const isAuthenticated = authService.isAuthenticated();
  if (isAuthenticated) {
    router.navigate(['/']);
  }
  return !isAuthenticated
};
