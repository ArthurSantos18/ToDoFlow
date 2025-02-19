import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { LoginRequest, RefreshRequest, RegisterRequest } from '../../../models/auth-response';
import { catchError, Observable, tap } from 'rxjs';
import { ApiResponseDual } from '../../../models/api-response';
import { API_ENDPOINTS } from '../../constants/api-config';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserRefreshToken } from '../../../models/user-refresh-token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isLoggedIn = signal<boolean>(this.hasToken());

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  login(loginRequest: LoginRequest): Observable<ApiResponseDual<string, UserRefreshToken>> {
    return this.http.post<ApiResponseDual<string, UserRefreshToken>>(`${API_ENDPOINTS.AUTH.LOGIN}`,loginRequest)
    .pipe(
      catchError((error) => {
        throw new Error(error.message);
      }),
      tap((response) => {
        if (response.data1 && response.data2 && response.success) {
          this.saveToken(response.data1, response.data2.refreshToken);
          this.isLoggedIn.update(() => true);
          localStorage.setItem('REFRESH_TOKEN_EXPIRATION', response.data2.expiration.toString());
        }
      })
    );
  }

  register(registerRequest: RegisterRequest): Observable<ApiResponseDual<string, UserRefreshToken>> {
    return this.http.post<ApiResponseDual<string, UserRefreshToken>>(`${API_ENDPOINTS.AUTH.REGISTER}`, registerRequest)
    .pipe(
      catchError((error) => {
        throw new Error(error.message);
      }),
      tap((response) => {
        if (response.data1 && response.data2 && response.success) {
          this.saveToken(response.data1, response.data2.refreshToken);
          this.isLoggedIn.update(() => true);
        }
      })
    )
  }

  refreshToken(): Observable<ApiResponseDual<string, UserRefreshToken>> {
    const refreshRequest: RefreshRequest = {refreshToken: this.getRefreshToken()!};

    return this.http.post<ApiResponseDual<string, UserRefreshToken>>("https://localhost:7215/api/auth/refresh", refreshRequest)
    .pipe(
      tap((response) => {
        if (response.data1 && response.data2 && response.success) {
          this.saveToken(response.data1, response.data2.refreshToken);
        }
      })
    )
  }

  saveToken(jwtToken: string, refreshToken: string): void {
    localStorage.setItem('USER_TOKEN', jwtToken);
    localStorage.setItem('REFRESH_TOKEN', refreshToken);
  }

  getJwtToken(): string | null {
    if (typeof window !== 'undefined' && typeof localStorage !== 'undefined') {
      return localStorage.getItem('USER_TOKEN');
    }
    return null;
  }

  getRefreshToken(): string | null {
    if (typeof window !== 'undefined' && typeof localStorage !== 'undefined') {
      return localStorage.getItem('REFRESH_TOKEN')
    }
    return null;
  }

  getSubFromToken(): string | null {
    const token = this.getJwtToken();
    if (!token) {
      return null;
    }

    try {
      const decoded = this.jwtHelper.decodeToken(token);
      return decoded.sub;
    }
    catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }

  getRoleFromToken(): string | null {
    const token = this.getJwtToken();
    if (!token) {
      return null;
    }

    try {
      const decoded = this.jwtHelper.decodeToken(token);
      return decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || null;
    }
    catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }

  isTokenExpired(): boolean {
    return this.jwtHelper.isTokenExpired(this.getJwtToken());
  }

  isRefreshTokenExpired(): boolean {
    const expirationDate = localStorage.getItem('REFRESH_TOKEN_EXPIRATION');

    if (!expirationDate) {
      return true;
    }

    const expirationTime = new Date(expirationDate).getTime();
    return Date.now() >= expirationTime;
  }


  logout(): void {
    localStorage.removeItem('USER_TOKEN');
    localStorage.removeItem('REFRESH_TOKEN');
    localStorage.removeItem('REFRESH_TOKEN_EXPIRATION');
    location.reload();
    this.isLoggedIn.update(() => false);
  }


  private hasToken(): boolean {
    const token = this.getJwtToken();
    if (token) {
      return true
    }
    return false;
  }

  getLoggedIn() {
    return this.isLoggedIn()
  }
}
