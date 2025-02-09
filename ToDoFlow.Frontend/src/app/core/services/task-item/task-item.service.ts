import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TaskItemCreateDto, TaskItemReadDto, TaskItemUpdateDto } from '../../../models/task-item';
import { ApiResponse } from '../../../models/api-response';
import { Observable } from 'rxjs';
import { API_ENDPOINTS } from '../../constants/api-config';

@Injectable({
  providedIn: 'root'
})
export class TaskItemService {
  constructor(private http: HttpClient) { }

  createTaskItem(TaskItemCreateDto: TaskItemCreateDto): Observable<ApiResponse<TaskItemReadDto[]>> {
    return this.http.post<ApiResponse<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.CREATE}`, TaskItemCreateDto)
  }

  getTaskItemByCategory(categoryId: number): Observable<ApiResponse<TaskItemReadDto[]>> {
    return this.http.get<ApiResponse<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.GET_BY_CATEGORY_ID}/${categoryId}`)
  }

  getTaskItemByUser(userId: number): Observable<ApiResponse<TaskItemReadDto[]>> {
    return this.http.get<ApiResponse<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.GET_BY_USER_ID}/${userId}`)
  }

  getTaskItemById(taskItemId: number): Observable<ApiResponse<TaskItemReadDto>> {
    return this.http.get<ApiResponse<TaskItemReadDto>>(`${API_ENDPOINTS.TASKITEM.GET_BY_ID}/${taskItemId}`)
  }

  updateTaskItem(taskItemUpdate: TaskItemUpdateDto): Observable<ApiResponse<TaskItemReadDto[]>> {
    return this.http.put<ApiResponse<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.UPDATE}/${taskItemUpdate.id}`, taskItemUpdate)
  }

  deleteTaskItem(taskItemId: number): Observable<ApiResponse<TaskItemReadDto[]>> {
    return this.http.delete<ApiResponse<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.DELETE}/${taskItemId}`)
  }
}
