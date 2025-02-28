import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TaskItemCreateDto, TaskItemReadDto, TaskItemUpdateDto } from '../../../models/task-item';
import { ApiResponseSingle } from '../../../models/api-response';
import { catchError, Observable } from 'rxjs';
import { API_ENDPOINTS } from '../../constants/api-config';
import { ErrorService } from '../error/error.service';

@Injectable({
  providedIn: 'root'
})
export class TaskItemService {
  constructor(private http: HttpClient, private errorService: ErrorService) { }

  createTaskItem(TaskItemCreateDto: TaskItemCreateDto): Observable<ApiResponseSingle<TaskItemReadDto[]>> {
    return this.http.post<ApiResponseSingle<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.CREATE}`, TaskItemCreateDto).
    pipe(
      catchError(this.errorService.handleError)
    )
  }

  getTaskItemByCategory(categoryId: number): Observable<ApiResponseSingle<TaskItemReadDto[]>> {
    return this.http.get<ApiResponseSingle<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.GET_BY_CATEGORY_ID}/${categoryId}`).
    pipe(
      catchError(this.errorService.handleError)
    )
  }

  getTaskItemByUser(userId: number): Observable<ApiResponseSingle<TaskItemReadDto[]>> {
    return this.http.get<ApiResponseSingle<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.GET_BY_USER_ID}/${userId}`).
    pipe(
      catchError(this.errorService.handleError)
    )
  }

  getTaskItemById(taskItemId: number): Observable<ApiResponseSingle<TaskItemReadDto>> {
    return this.http.get<ApiResponseSingle<TaskItemReadDto>>(`${API_ENDPOINTS.TASKITEM.GET_BY_ID}/${taskItemId}`).
    pipe(
      catchError(this.errorService.handleError)
    )
  }

  updateTaskItem(taskItemUpdate: TaskItemUpdateDto): Observable<ApiResponseSingle<TaskItemReadDto[]>> {
    return this.http.put<ApiResponseSingle<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.UPDATE}/${taskItemUpdate.id}`, taskItemUpdate).
    pipe(
      catchError(this.errorService.handleError)
    )
  }

  deleteTaskItem(taskItemId: number): Observable<ApiResponseSingle<TaskItemReadDto[]>> {
    return this.http.delete<ApiResponseSingle<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.DELETE}/${taskItemId}`).
    pipe(
      catchError(this.errorService.handleError)
    )
  }
}
