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
  private tokenCheckInterval = 60000;

  isLoggedIn = signal<boolean>(this.hasToken());

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) {
    //this.startTokenAutoCheck()
  }

  login(loginRequest: LoginRequest) {
    return this.http.post<ApiResponse<string>>(`${API_ENDPOINTS.AUTH.LOGIN}`,loginRequest)
    .pipe(
      catchError((error) => {
        throw new Error(error.message);
      }),
      tap((response) => {
        if (response.data && response.success) {
          this.saveToken(response.data);
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
          this.isLoggedIn.update(() => true);
        }
      })
    )
  }

  logout(): void {
    localStorage.removeItem('USER_TOKEN');
    this.isLoggedIn.update(() => false);
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

  /*isTokenExpired(): boolean {
    const token = localStorage.getItem('USER_TOKEN');
    return this.jwtHelper.isTokenExpired(token);
  }*/

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

  /*startTokenAutoCheck() {
    if(this.getLoggedIn()) {
      setInterval(() => {
        if (this.isTokenExpired()) {
          console.log(this.isTokenExpired())
          this.logout();
        }
      }, this.tokenCheckInterval);
    }
  }*/
}
