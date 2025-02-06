import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { TaskItemService } from '../../../../core/services/task-item/task-item.service';

declare const bootstrap: any;

@Component({
  selector: 'app-task-item-delete-modal',
  standalone: true,
  imports: [],
  templateUrl: './task-item-delete-modal.component.html',
  styleUrl: './task-item-delete-modal.component.css'
})
export class TaskITemDeleteModalComponent {
  @ViewChild('ItaskItemDeleteModal') modalElement!: ElementRef;

  @Input() taskItemName: string | null = null;
  @Input() taskItemId: number | null = null;

  constructor(private taskItemService: TaskItemService) { }

  openTaskItemDeleteModal(): void {
    if(this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.show();
    }
    else {
      console.error('Modal element not found!');
    }
  };

  closeTaskItemDeleteModal(): void {
    if(this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.hide();
    }
    else {
      console.error('Modal element not found!');
    }
  };

  deleteTaskItem(): void {
    this.taskItemService.deleteTaskItem(Number(this.taskItemId)).subscribe({
      next: () => {
        location.reload()
      },
      error: (error) => console.error('')
    })
  }
}
