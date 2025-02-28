import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { TaskItemService } from '../../../../core/services/task-item/task-item.service';
import { CommonModule } from '@angular/common';

declare const bootstrap: any;

@Component({
  selector: 'app-task-item-delete-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './task-item-delete-modal.component.html',
  styleUrl: './task-item-delete-modal.component.css'
})
export class TaskITemDeleteModalComponent {
  @ViewChild('ItaskItemDeleteModal') modalElement!: ElementRef;

  @Input() taskItemName: string | null = null;
  @Input() taskItemId: number | null = null;

  errorMessage: string | null = null;
  modal: any

  constructor(private taskItemService: TaskItemService) { }

  openTaskItemDeleteModal(): void {
    if(this.modalElement && this.modalElement.nativeElement) {
      this.modal = new bootstrap.Modal(this.modalElement.nativeElement);
      this.modal.show();
    }
    else {
      console.error('Modal element not found!');
    }
  };

  closeTaskItemDeleteModal(): void {
    if(this.modalElement && this.modalElement.nativeElement) {
      this.modal.hide();
    }
    else {
      console.error('Modal element not found!');
    }
  };

  deleteTaskItem(): void {
    this.taskItemService.deleteTaskItem(this.taskItemId!).subscribe({
      next: (response) => {
        if(response.success === false) {
          this.errorMessage = response.message
        }
        else {
          this.errorMessage = null
          location.reload()
        }
      },
      error: (err) => {
        this.errorMessage = err
      }
    })
  }
}
