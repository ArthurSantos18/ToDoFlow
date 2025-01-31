import { Component, ElementRef, ViewChild } from '@angular/core';

declare var bootstrap: any;

@Component({
  selector: 'app-category-create-modal',
  standalone: true,
  imports: [],
  templateUrl: './category-create-modal.component.html',
  styleUrl: './category-create-modal.component.css'
})
export class CategoryCreateModalComponent {
  @ViewChild('IcategoryCreateModal') modalElement !: ElementRef

  OpenCategoryCreateModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.show();
    }
    else {
      console.error('Modal element not found!');
    }
  }

  CloseCategoryCreateModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.hide();
    }
    else {
      console.error('Modal element not found!')
    }
  }
}
