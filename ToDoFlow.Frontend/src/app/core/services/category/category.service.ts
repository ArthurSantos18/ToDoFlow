import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponseSingle } from '../../../models/api-response';
import { CategoryCreateDto, CategoryReadDto, CategoryUpdateDto } from '../../../models/category';
import { catchError, Observable } from 'rxjs';
import { API_ENDPOINTS } from '../../constants/api-config';
import { ErrorService } from '../error/error.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient, private errorService: ErrorService) { }

  createCategory(categoryCreateDto: CategoryCreateDto): Observable<ApiResponseSingle<CategoryReadDto[]>> {
    return this.http.post<ApiResponseSingle<CategoryReadDto[]>>(`${API_ENDPOINTS.CATEGORY.CREATE}`, categoryCreateDto).
    pipe(
      catchError(this.errorService.handleError)
    );
  }

  getAllCategories(): Observable<ApiResponseSingle<CategoryReadDto[]>> {
    return this.http.get<ApiResponseSingle<CategoryReadDto[]>>(`${API_ENDPOINTS.CATEGORY.GET_ALL}`).
    pipe(
      catchError(this.errorService.handleError)
    );
  }

  getCategoryByUser(userId: number): Observable<ApiResponseSingle<CategoryReadDto[]>> {
    return this.http.get<ApiResponseSingle<CategoryReadDto[]>>(`${API_ENDPOINTS.CATEGORY.GET_BY_USER_ID}/${userId}`).
    pipe(
      catchError(this.errorService.handleError)
    );
  }

  getCategoryById(categoryId: number): Observable<ApiResponseSingle<CategoryReadDto[]>> {
    return this.http.get<ApiResponseSingle<CategoryReadDto[]>>(`${API_ENDPOINTS.CATEGORY.GET_BY_ID}/${categoryId}`).
    pipe(
      catchError(this.errorService.handleError)
    );
  }

  updateCategory(categoryUpdateDto: CategoryUpdateDto): Observable<ApiResponseSingle<CategoryReadDto[]>> {
    return this.http.put<ApiResponseSingle<CategoryReadDto[]>>(`${API_ENDPOINTS.CATEGORY.UPDATE}/${categoryUpdateDto.id}`, categoryUpdateDto).
    pipe(
      catchError(this.errorService.handleError)
    );
  }

  deleteCategory(categoryId: number): Observable<ApiResponseSingle<CategoryReadDto[]>> {
    return this.http.delete<ApiResponseSingle<CategoryReadDto[]>>(`${API_ENDPOINTS.CATEGORY.DELETE}/${categoryId}`).
    pipe(
      catchError(this.errorService.handleError)
    );
  }
}
