import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../services/auth/auth.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const role = authService.getRoleFromToken();
  const requiredRole = route.data?.['role'];

  
  if (!authService.getLoggedIn()) {
    router.navigate(['login']);
    return false;
  }

  if(authService.isTokenExpired()) {
    authService.refreshToken().subscribe();
  }

  if(authService.isRefreshTokenExpired()){
    authService.logout();
    router.navigate(['login'])
  }

  if (requiredRole && role !== requiredRole) {
    router.navigate(['home']);
    return false;
  }

  return true;
};
