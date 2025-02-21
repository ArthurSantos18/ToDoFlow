import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { JWT_OPTIONS, JwtModule } from '@auth0/angular-jwt';
import { authInterceptor } from './core/interceptors/auth/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideClientHydration(),
    provideHttpClient(withInterceptors([authInterceptor]), withFetch()),
    importProvidersFrom(
    JwtModule.forRoot({
      jwtOptionsProvider: {
        provide: JWT_OPTIONS,
        useValue: {
          tokenGetter: () => localStorage.getItem('USER_TOKEN'),
          allowedDomains: ['http://localhost:4200'],
          disallowedDomain: ['http://localhost:4200/login ']
        }
      }
    }))
  ]
};
