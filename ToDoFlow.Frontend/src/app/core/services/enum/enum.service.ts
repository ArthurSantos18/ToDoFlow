import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../models/api-response';
import { API_ENDPOINTS } from '../../constants/api-config';

@Injectable({
  providedIn: 'root'
})
export class EnumService {

  constructor(private http: HttpClient) { }

  getPriority(): Observable<ApiResponse<{[key: number]: string}>> {
    return this.http.get<ApiResponse<{[key: number]: string}>>(`${API_ENDPOINTS.ENUM.GET_PRIORITY}`)
  }
}
