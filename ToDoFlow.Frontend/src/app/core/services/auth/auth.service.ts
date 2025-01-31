import { HttpClient } from '@angular/common/http';
import { computed, Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';
import { LoginRequest, RegisterRequest } from '../../../models/auth-response';
import { catchError, map, Observable } from 'rxjs';
import { ApiResponse } from '../../../models/api-response';
import { API_ENDPOINTS } from '../../constants/api-config';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isLoggedIn = signal<boolean>(this.hasToken());

  constructor(private http: HttpClient, private router: Router) {}

  login(loginRequest: LoginRequest) {
    return this.http.post<any>(`${API_ENDPOINTS.AUTH.LOGIN}`,loginRequest)
    .pipe(
      catchError((error) => {
        throw new Error(error.message);
      }),
      map((response) => {
        if (response.data && response.success) {
          this.saveToken(response.data);
          this.isLoggedIn.update(() => true);
          this.router.navigate(['home']);
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
      map((response) => {
        if (response.data && response.success) {
          this.saveToken(response.data);
          this.isLoggedIn.update(() => true);
          this.router.navigate(['home'])
        }

      })
    )
  }

  logout(): void {
    localStorage.removeItem('USER_TOKEN');
    this.isLoggedIn.update(() => false);
    this.router.navigate(['login']);
  }

  saveToken(token: string): void {
    localStorage.setItem('USER_TOKEN', token);
  }

  getToken(): string | null {
    return localStorage.getItem('USER_TOKEN');
  }

  private hasToken(): boolean {
    return !!this.getToken();
  }

  getLoggedIn() {
    return this.isLoggedIn()
  }
}
