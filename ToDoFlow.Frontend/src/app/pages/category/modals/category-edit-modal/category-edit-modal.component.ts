import { Component, ElementRef, ViewChild } from '@angular/core';

declare const bootstrap: any;

@Component({
  selector: 'app-category-edit-modal',
  standalone: true,
  imports: [],
  templateUrl: './category-edit-modal.component.html',
  styleUrl: './category-edit-modal.component.css'
})
export class CategoryEditModalComponent {
  @ViewChild('IcategoryEditModal') modalElement!: ElementRef

  OpenCategoryEditModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.show();
    }
    else {
      console.error('Modal element not found!');
    }
  }

  CloseCategoryEditModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.hide();
    }
    else {
      console.error('Modal element not found!')
    }
  }

}
