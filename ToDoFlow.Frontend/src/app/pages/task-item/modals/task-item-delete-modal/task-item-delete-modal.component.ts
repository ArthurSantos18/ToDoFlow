import { Component, ElementRef, ViewChild } from '@angular/core';

declare const bootstrap: any;

@Component({
  selector: 'app-task-item-delete-modal',
  standalone: true,
  imports: [],
  templateUrl: './task-item-delete-modal.component.html',
  styleUrl: './task-item-delete-modal.component.css'
})
export class TaskITemDeleteModalComponent {
  @ViewChild('ItaskItemDeleteModal') modalElement!: ElementRef

  OpenTaskItemDeleteModal(): void {
    if(this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.show();
    }
    else {
      console.error('Modal element not found!');
    }
  };

  CloseTaskItemDeleteModal(): void {
    if(this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.hide();
    }
    else {
      console.error('Modal element not found!');
    }
  };
}
