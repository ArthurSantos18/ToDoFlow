import { Component, ElementRef, ViewChild } from '@angular/core';

declare var bootstrap: any;

@Component({
  selector: 'app-category-delete-modal',
  standalone: true,
  imports: [],
  templateUrl: './category-delete-modal.component.html',
  styleUrl: './category-delete-modal.component.css'
})
export class CategoryDeleteModalComponent {
  @ViewChild('IcategoryDeleteModal') modalElement !: ElementRef

  OpenCategoryDeleteModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.show();
    }
    else {
      console.error('Modal element not found!');
    }
  }

  CloseCategoryDeleteModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.hide();
    }
    else {
      console.error('Modal element not found!')
    }
  }
}
