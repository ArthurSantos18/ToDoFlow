import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { TaskItemDetailsModalComponent } from './modals/task-item-details-modal/task-item-details-modal.component';
import { TaskITemDeleteModalComponent } from './modals/task-item-delete-modal/task-item-delete-modal.component';
import { CategoryService } from '../../core/services/category/category.service';
import { AuthService } from '../../core/services/auth/auth.service';
import { CategoryReadDto } from '../../models/category';
import { CommonModule, DatePipe } from '@angular/common';
import { TaskItemService } from '../../core/services/task-item/task-item.service';
import { TaskItemReadDto } from '../../models/task-item';
import { TaskItemEditComponent } from './task-item-edit/task-item-edit.component';

@Component({
  selector: 'app-task-item',
  standalone: true,
  imports: [TaskItemDetailsModalComponent, TaskITemDeleteModalComponent, CommonModule, RouterModule],
  templateUrl: './task-item.component.html',
  styleUrl: './task-item.component.css',
  providers: [DatePipe]
})
export class TaskItemComponent implements OnInit {
  @ViewChild(TaskItemDetailsModalComponent) taskItemDetailsModal!: TaskItemDetailsModalComponent;
  @ViewChild(TaskITemDeleteModalComponent) taskItemDeleteModal!: TaskITemDeleteModalComponent;
  @ViewChild(TaskItemEditComponent) taskItemEdit!: TaskItemEditComponent;

  userId: number | null = null;
  errorMessage: string | null = null;
  categoriesByUserId: CategoryReadDto[] = []
  taskItemByCategory:  {[categoryId: number]: TaskItemReadDto[] } = {}

  constructor(private categoryService: CategoryService, private taskItemService: TaskItemService, private authService: AuthService, private datePipe: DatePipe) { }

  ngOnInit(): void {
    this.userId = Number(this.authService.getSubFromToken());
    this.loadCategories(this.userId)
  }

  loadCategories(userId: number): void {
    this.categoryService.getCategoryByUser(userId).subscribe({
      next: (response) => {
        if (response.success === false) {
          this.errorMessage = response.message;
        }
        else {
          this.errorMessage = null;
          this.categoriesByUserId = response.data;

          this.categoriesByUserId.forEach(category => {
            this.loadTaskItemsByCategory(category.id);
          });
        }
      },
      error: (err) => {
        this.errorMessage = err;
      }
    });
  }

  loadTaskItemsByCategory(categoryId: number): void {
    this.taskItemService.getTaskItemByCategory(categoryId).subscribe({
      next: (response) => {
        if (response.success === false) {
          this.errorMessage = response.message;
        }
        else {
          this.errorMessage = null;
          this.taskItemByCategory[categoryId] = response.data
        }
      },
      error: (err) => {
        this.errorMessage = err;
      }
    });
  }

  openTaskItemDetailsModal(taskItemId: number): void {
    this.taskItemService.getTaskItemById(taskItemId).subscribe({
      next: (response) => {
        if (response.success === false) {
          this.errorMessage = response.message;
        }
        else {
          this.errorMessage = null;

          this.taskItemDetailsModal.taskItemDetails = response.data

          const formattedCreatedData = this.datePipe.transform(this.taskItemDetailsModal.taskItemDetails.createdAt, 'MM-dd-yyyy');
          const formattedCompletedData = this.datePipe.transform(this.taskItemDetailsModal.taskItemDetails.completeAt, 'MM-dd-yyyy');
          this.taskItemDetailsModal.formattedCreatedData = formattedCreatedData
          this.taskItemDetailsModal.formattedCompletedData = formattedCompletedData

          const category = this.categoriesByUserId.find(cat => cat.id === response.data.categoryId)
          if (category) {
            this.taskItemDetailsModal.categoryName = category?.name
          }
        }
      },
      error: (err) => {
        this.errorMessage = err;
      }
    });

    this.taskItemDetailsModal.openTaskItemDetailsModal();
  }

  openTaskItemDeleteModal(taskItemName: string, taskItemId: number): void {
    this.taskItemDeleteModal.taskItemName = taskItemName
    this.taskItemDeleteModal.taskItemId = taskItemId
    this.taskItemDeleteModal.openTaskItemDeleteModal();
  }
}
