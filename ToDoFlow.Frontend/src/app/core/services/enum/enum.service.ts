import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { ApiResponseSingle } from '../../../models/api-response';
import { API_ENDPOINTS } from '../../constants/api-config';
import { ErrorService } from '../error/error.service';

@Injectable({
  providedIn: 'root'
})
export class EnumService {

  constructor(private http: HttpClient, private errorService: ErrorService) { }

  getPriority(): Observable<ApiResponseSingle<{[key: number]: string}>> {
    return this.http.get<ApiResponseSingle<{[key: number]: string}>>(`${API_ENDPOINTS.ENUM.GET_PRIORITY}`).
      pipe(
        catchError(this.errorService.handleError)
      );
  }
}
