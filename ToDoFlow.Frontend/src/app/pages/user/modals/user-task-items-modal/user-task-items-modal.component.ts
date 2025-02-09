import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { TaskItemReadDto } from '../../../../models/task-item';
import { CommonModule } from '@angular/common';

declare const bootstrap: any

@Component({
  selector: 'app-user-task-items-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-task-items-modal.component.html',
  styleUrl: './user-task-items-modal.component.css'
})
export class UserTaskItemsModalComponent {
  @ViewChild("IUserTaskItemsModal") modalElement!: ElementRef

  @Input() userName: string | null = null;
  @Input() taskItemsByUserId: TaskItemReadDto[] = []

  modal: any

  openUserTaskItemModal() {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal = new bootstrap.Modal(this.modalElement.nativeElement)
      this.modal.show()
    }
    else {
      console.error('Modal element not found!');
    }
  }

  closeUserTaskItemModal() {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal.hide()
    }
    else {
      console.error('Modal Element not found!')
    }
  }
}
