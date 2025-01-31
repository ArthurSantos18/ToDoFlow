import { Component, ViewChild } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TaskItemDetailsModalComponent } from './modals/task-item-details-modal/task-item-details-modal.component';
import { TaskITemDeleteModalComponent } from './modals/task-item-delete-modal/task-item-delete-modal.component';

@Component({
  selector: 'app-task-item',
  standalone: true,
  imports: [RouterModule, TaskItemDetailsModalComponent, TaskITemDeleteModalComponent],
  templateUrl: './task-item.component.html',
  styleUrl: './task-item.component.css'
})
export class TaskItemComponent {
  @ViewChild(TaskItemDetailsModalComponent) taskItemDetailsModal!: TaskItemDetailsModalComponent
  @ViewChild(TaskITemDeleteModalComponent) taskItemDeleteModal!: TaskITemDeleteModalComponent

  OpenTaskItemDetailsModal() {
    this.taskItemDetailsModal.OpenTaskItemDetailsModal();
  }

  OpenTaskItemDeleteModal() {
    this.taskItemDeleteModal.OpenTaskItemDeleteModal();
  }
}
