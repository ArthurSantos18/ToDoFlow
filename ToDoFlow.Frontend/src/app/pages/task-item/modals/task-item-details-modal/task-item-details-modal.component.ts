import { Component, ElementRef, Input,  ViewChild } from '@angular/core';
import { TaskItemReadDto } from '../../../../models/task-item';

declare const bootstrap: any;

@Component({
  selector: 'app-task-item-details-modal',
  standalone: true,
  imports: [],
  templateUrl: './task-item-details-modal.component.html',
  styleUrl: './task-item-details-modal.component.css'
})
export class TaskItemDetailsModalComponent {
  @ViewChild('ItaskItemDetailsModal') modalElement !: ElementRef
  @Input() taskItemDetails: TaskItemReadDto | null = null;
  @Input() formattedCreatedData: string | null = null;
  @Input() formattedCompletedData: string | null = null;
  @Input() categoryName: string | null = null;

  modal: any

  openTaskItemDetailsModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal = new bootstrap.Modal(this.modalElement.nativeElement);
      this.modal.show();
    }
    else {
      console.error('Modal element not found!');
    }
  };

  closeTaskItemDetailsModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal.hide();
    }
    else {
      console.error('Modal element not found!');
    }
  };
}
