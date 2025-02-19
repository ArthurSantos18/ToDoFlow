import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TaskItemCreateDto, TaskItemReadDto, TaskItemUpdateDto } from '../../../models/task-item';
import { ApiResponseSingle } from '../../../models/api-response';
import { Observable } from 'rxjs';
import { API_ENDPOINTS } from '../../constants/api-config';

@Injectable({
  providedIn: 'root'
})
export class TaskItemService {
  constructor(private http: HttpClient) { }

  createTaskItem(TaskItemCreateDto: TaskItemCreateDto): Observable<ApiResponseSingle<TaskItemReadDto[]>> {
    return this.http.post<ApiResponseSingle<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.CREATE}`, TaskItemCreateDto)
  }

  getTaskItemByCategory(categoryId: number): Observable<ApiResponseSingle<TaskItemReadDto[]>> {
    return this.http.get<ApiResponseSingle<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.GET_BY_CATEGORY_ID}/${categoryId}`)
  }

  getTaskItemByUser(userId: number): Observable<ApiResponseSingle<TaskItemReadDto[]>> {
    return this.http.get<ApiResponseSingle<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.GET_BY_USER_ID}/${userId}`)
  }

  getTaskItemById(taskItemId: number): Observable<ApiResponseSingle<TaskItemReadDto>> {
    return this.http.get<ApiResponseSingle<TaskItemReadDto>>(`${API_ENDPOINTS.TASKITEM.GET_BY_ID}/${taskItemId}`)
  }

  updateTaskItem(taskItemUpdate: TaskItemUpdateDto): Observable<ApiResponseSingle<TaskItemReadDto[]>> {
    return this.http.put<ApiResponseSingle<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.UPDATE}/${taskItemUpdate.id}`, taskItemUpdate)
  }

  deleteTaskItem(taskItemId: number): Observable<ApiResponseSingle<TaskItemReadDto[]>> {
    return this.http.delete<ApiResponseSingle<TaskItemReadDto[]>>(`${API_ENDPOINTS.TASKITEM.DELETE}/${taskItemId}`)
  }
}
