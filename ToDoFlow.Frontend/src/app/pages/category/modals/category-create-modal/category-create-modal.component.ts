import { Component, ElementRef, output, Output, OutputEmitterRef, ViewChild } from '@angular/core';
import { CategoryService } from '../../../../core/services/category/category.service';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth/auth.service';

declare var bootstrap: any;

@Component({
  selector: 'app-category-create-modal',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './category-create-modal.component.html',
  styleUrl: './category-create-modal.component.css'
})
export class CategoryCreateModalComponent {
  @ViewChild('IcategoryCreateModal') modalElement !: ElementRef;

  modal: any
  categoryCreateForm: FormGroup

  constructor(private categoryService: CategoryService, private authService: AuthService, private fb: FormBuilder) {
    this.categoryCreateForm = this.fb.group({
      userId: new FormControl(Number(authService.getSubFromToken())),
      name: new FormControl('', [Validators.required])
    })
  }

  openCategoryCreateModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal = new bootstrap.Modal(this.modalElement.nativeElement);
      this.modal.show();
    }
    else {
      console.error('Modal element not found!');
    }
  }

  closeCategoryCreateModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal.hide();
    }
    else {
      console.error('Modal element not found!')
    }
  }

  createCategory(): void {
    this.categoryService.createCategory(this.categoryCreateForm.value).subscribe({
      next: () => {
        location.reload()
      },
      error: (error) => console.error('')
    })
  }
}
