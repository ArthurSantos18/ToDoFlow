import { Component, ElementRef, Input, input, OnInit, output, OutputEmitterRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoryService } from '../../../../core/services/category/category.service';
import { CommonModule } from '@angular/common';

declare const bootstrap: any;

@Component({
  selector: 'app-category-edit-modal',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './category-edit-modal.component.html',
  styleUrl: './category-edit-modal.component.css'
})
export class CategoryEditModalComponent {
  @ViewChild('IcategoryEditModal') modalElement!: ElementRef;
  @Input() categoryId: number | null = null;
  @Input() categoryName: string | null = null;

  modal: any
  categoryUpdateForm: FormGroup;
  errorMessage: string | null = null;

  constructor(private categoryService: CategoryService, private fb: FormBuilder) {

    this.categoryUpdateForm = this.fb.group({
      id: new FormControl(''),
      name: new FormControl('', [Validators.required])
    })
  }

  openCategoryEditModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal = new bootstrap.Modal(this.modalElement.nativeElement);
      this.modal.show();

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
      this.modal.hide();
    }
    else {
      console.error('Modal element not found!')
    }
  }

  updateCategory(): void {
    if(this.categoryUpdateForm.valid) {
      this.categoryService.updateCategory(this.categoryUpdateForm.value).subscribe({
        next: (response) => {
          if (response.success === false) {
            this.errorMessage = response.message;
          }
          else {
            this.errorMessage = null;
            location.reload()
          }
        },
        error: (err) => {
          this.errorMessage = err
        }
      });
    }
  }
}
