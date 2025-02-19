import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';
import { catchError, retry, throwError } from 'rxjs';
import { Router } from '@angular/router';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService)
  const router = inject(Router)

  if(authService.isLoggedIn()) {
    if(authService.isRefreshTokenExpired()) {
      authService.logout()
      router.navigate(['login'])
    }
    else {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${authService.getJwtToken()}`
        }
      })
    }
  }

  return next(req).pipe(
    retry(2),
    catchError((e: HttpErrorResponse) => {
      if(e.status === 401) {
        router.navigate(['home'])
      }
      const error = e.error.message || e.statusText
      return throwError(() => error)
    })
  );
};
