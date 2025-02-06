import { Component, ElementRef, Input, input, OnInit, output, OutputEmitterRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth/auth.service';
import { CategoryService } from '../../../../core/services/category/category.service';

declare const bootstrap: any;

@Component({
  selector: 'app-category-edit-modal',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './category-edit-modal.component.html',
  styleUrl: './category-edit-modal.component.css'
})
export class CategoryEditModalComponent {
  @ViewChild('IcategoryEditModal') modalElement!: ElementRef;
  @Input() categoryId: number | null = null;
  @Input() categoryName: string | null = null;

  categoryUpdateForm: FormGroup;

  constructor(private categoryService: CategoryService, private fb: FormBuilder) {

    this.categoryUpdateForm = this.fb.group({
      id: new FormControl(''),
      name: new FormControl('', [Validators.required])
    })
  }

  openCategoryEditModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.show();

      if (this.categoryId !== null) {
        this.categoryUpdateForm.patchValue({
          id: this.categoryId
        });
    }
  }
    else {
      console.error('Modal element not found!');
    }
  }

  closeCategoryEditModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      const modal = new bootstrap.Modal(this.modalElement.nativeElement);
      modal.hide();
    }
    else {
      console.error('Modal element not found!')
    }
  }

  updateCategory(): void {
    this.categoryService.updateCategory(this.categoryUpdateForm.value).subscribe({
      next: () => {
        location.reload()
      },
      error: (error) => console.error('')
    })
  }
}
