import { Component, ElementRef, ViewChild } from '@angular/core';

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

  OpenTaskItemDetailsModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.show();
    }
    else {
      console.error('Modal element not found!');
    }
  };

  CloseTaskItemDetailsModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.hide();
    }
    else {
      console.error('Modal element not found!');
    }
  };
}
