import {CanActivateFn, Router} from '@angular/router';
import {AuthService} from '../services/auth.service';
import {inject} from "@angular/core";

export const authGuard : CanActivateFn = () => {
    console.log("dupa")
    const authService = inject(AuthService)
    const router = inject(Router)

    const isAuthenticated = authService.isAuthenticated();
    if (!isAuthenticated) {
      router.navigate(['/login']);
    }
    return isAuthenticated
  };

