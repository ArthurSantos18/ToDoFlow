import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponseSingle } from '../../../models/api-response';
import { UserEditDto, UserReadDto } from '../../../models/user';
import { API_ENDPOINTS } from '../../constants/api-config';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<ApiResponseSingle<UserReadDto[]>> {
    return this.http.get<ApiResponseSingle<UserReadDto[]>>(`${API_ENDPOINTS.USER.GET}`)
  }

  getUserById(userId: number): Observable<ApiResponseSingle<UserReadDto>> {
    return this.http.get<ApiResponseSingle<UserReadDto>>(`${API_ENDPOINTS.USER.GET_BY_ID}/${userId}`)
  }

  updateUser(UserEditDto: UserEditDto): Observable<ApiResponseSingle<UserReadDto[]>> {
    return this.http.put<ApiResponseSingle<UserReadDto[]>>(`${API_ENDPOINTS.USER.PUT}/${UserEditDto.id}`, UserEditDto)
  }

  deleteUser(userId: number): Observable<ApiResponseSingle<UserReadDto[]>> {
    return this.http.delete<ApiResponseSingle<UserReadDto[]>>(`${API_ENDPOINTS.USER.DELETE}/${userId}`)
  }
}
