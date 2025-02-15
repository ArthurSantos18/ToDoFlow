import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { LoginRequest, RegisterRequest } from '../../../models/auth-response';
import { catchError, map, tap } from 'rxjs';
import { ApiResponse } from '../../../models/api-response';
import { API_ENDPOINTS } from '../../constants/api-config';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenExpirationTimer: any;

  isLoggedIn = signal<boolean>(this.hasToken());

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }

  login(loginRequest: LoginRequest) {
    return this.http.post<ApiResponse<string>>(`${API_ENDPOINTS.AUTH.LOGIN}`,loginRequest)
    .pipe(
      catchError((error) => {
        throw new Error(error.message);
      }),
      tap((response) => {
        if (response.data && response.success) {
          this.saveToken(response.data);
          this.startTokenTimer();
          this.isLoggedIn.update(() => true);
        }
      })
    );
  }

  register(registerRequest: RegisterRequest) {
    return this.http.post<ApiResponse<string>>(`${API_ENDPOINTS.AUTH.REGISTER}`, registerRequest)
    .pipe(
      catchError((error) => {
        throw new Error(error.message);
      }),
      tap((response) => {
        if (response.data && response.success) {
          this.saveToken(response.data);
          this.startTokenTimer();
          this.isLoggedIn.update(() => true);
        }
      })
    )
  }

  saveToken(token: string): void {
    localStorage.setItem('USER_TOKEN', token);
  }

  getToken(): string | null {
    if (typeof window !== 'undefined' && typeof localStorage !== 'undefined') {
      return localStorage.getItem('USER_TOKEN');
    }
    return null;
  }

  getSubFromToken(): string | null {
    const token = this.getToken();
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
    const token = this.getToken();
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

  getTokenExpiration(): Date | null {
    try {
      return this.jwtHelper.getTokenExpirationDate(this.getToken()!);
    }
    catch {
      return null;
    }
  }

  isTokenExpired(): boolean {
    return this.jwtHelper.isTokenExpired(this.getToken());
  }

  startTokenTimer() {
    const expirationDate = this.getTokenExpiration();
    if (!expirationDate) {
      return;
    }

    var expiresIn = expirationDate.getTime() - Date.now();
    console.log(expiresIn)
    this.tokenExpirationTimer = setTimeout(() => this.logout(), expiresIn);
  }

  stopTokenTimer() {
    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
      this.tokenExpirationTimer = null;
    }
  }

  logout(): void {
    localStorage.removeItem('USER_TOKEN');
    this.stopTokenTimer();
    this.isLoggedIn.update(() => false);
  }


  private hasToken(): boolean {
    const token = this.getToken();
    if (token) {
      return true
    }
    return false;
  }

  getLoggedIn() {
    return this.isLoggedIn()
  }
}
