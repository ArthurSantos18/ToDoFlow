import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../models/api-response';
import { UserReadDto } from '../../../models/user';
import { API_ENDPOINTS } from '../../constants/api-config';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<ApiResponse<UserReadDto[]>> {
    return this.http.get<ApiResponse<UserReadDto[]>>(`${API_ENDPOINTS.USER.GET}`)
  }

  getUserById(userId: number): Observable<ApiResponse<UserReadDto>> {
    return this.http.get<ApiResponse<UserReadDto>>(`${API_ENDPOINTS.USER.GET_BY_ID}/${userId}`)
  }

  deleteUser(userId: number): Observable<ApiResponse<UserReadDto[]>> {
    return this.http.delete<ApiResponse<UserReadDto[]>>(`${API_ENDPOINTS.USER.DELETE}/${userId}`)
  }
}
