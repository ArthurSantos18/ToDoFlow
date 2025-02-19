import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponseSingle } from '../../../models/api-response';
import { CategoryCreateDto, CategoryReadDto, CategoryUpdateDto } from '../../../models/category';
import { Observable } from 'rxjs';
import { API_ENDPOINTS } from '../../constants/api-config';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  createCategory(categoryCreateDto: CategoryCreateDto): Observable<ApiResponseSingle<CategoryReadDto[]>> {
    return this.http.post<ApiResponseSingle<CategoryReadDto[]>>(`${API_ENDPOINTS.CATEGORY.CREATE}`, categoryCreateDto)
  }

  getAllCategories(): Observable<ApiResponseSingle<CategoryReadDto[]>> {
    return this.http.get<ApiResponseSingle<CategoryReadDto[]>>(`${API_ENDPOINTS.CATEGORY.GET_ALL}`)
  }

  getCategoryByUser(userId: number): Observable<ApiResponseSingle<CategoryReadDto[]>> {
    return this.http.get<ApiResponseSingle<CategoryReadDto[]>>(`${API_ENDPOINTS.CATEGORY.GET_BY_USER_ID}/${userId}`);
  }

  getCategoryById(categoryId: number): Observable<ApiResponseSingle<CategoryReadDto[]>> {
    return this.http.get<ApiResponseSingle<CategoryReadDto[]>>(`${API_ENDPOINTS.CATEGORY.GET_BY_ID}/${categoryId}`)
  }

  updateCategory(categoryUpdateDto: CategoryUpdateDto): Observable<ApiResponseSingle<CategoryReadDto[]>> {
    return this.http.put<ApiResponseSingle<CategoryReadDto[]>>(`${API_ENDPOINTS.CATEGORY.UPDATE}/${categoryUpdateDto.id}`, categoryUpdateDto)
  }

  deleteCategory(categoryId: number): Observable<ApiResponseSingle<CategoryReadDto[]>> {
    return this.http.delete<ApiResponseSingle<CategoryReadDto[]>>(`${API_ENDPOINTS.CATEGORY.DELETE}/${categoryId}`)
  }
}
