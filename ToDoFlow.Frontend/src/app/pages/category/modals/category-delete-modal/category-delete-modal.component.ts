import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { CategoryService } from '../../../../core/services/category/category.service';

declare var bootstrap: any;

@Component({
  selector: 'app-category-delete-modal',
  standalone: true,
  imports: [],
  templateUrl: './category-delete-modal.component.html',
  styleUrl: './category-delete-modal.component.css'
})
export class CategoryDeleteModalComponent {
  @ViewChild('IcategoryDeleteModal') modalElement !: ElementRef;

  @Input() categoryId: number | null = null;
  @Input() categoryName: string | null = null;

  modal: any

  constructor(private categoryService: CategoryService) {}

  openCategoryDeleteModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal = new bootstrap.Modal(this.modalElement.nativeElement);
      this.modal.show();
    }
    else {
      console.error('Modal element not found!');
    }
  }

  closeCategoryDeleteModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal.hide();
    }
    else {
      console.error('Modal element not found!')
    }
  }

  deleteCategory(): void {
    this.categoryService.deleteCategory(this.categoryId!).subscribe({
      next: () => {
        location.reload()
      },
      error: (error) => console.error('')
    })
  }
}
