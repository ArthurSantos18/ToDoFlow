import { Component, ViewChild } from '@angular/core';
import { CategoryCreateModalComponent } from './modals/category-create-modal/category-create-modal.component';
import { CategoryEditModalComponent } from './modals/category-edit-modal/category-edit-modal.component';
import { CategoryDeleteModalComponent } from './modals/category-delete-modal/category-delete-modal.component';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [CategoryCreateModalComponent, CategoryEditModalComponent, CategoryDeleteModalComponent],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent {
  @ViewChild (CategoryCreateModalComponent) categoryCreateModal!: CategoryCreateModalComponent
  @ViewChild (CategoryEditModalComponent) categoryEditModal!: CategoryEditModalComponent
  @ViewChild (CategoryDeleteModalComponent) categoryDeleteModal!: CategoryDeleteModalComponent

  OpenCategoryCreateModal() {
    this.categoryCreateModal.OpenCategoryCreateModal();
  }

  OpenCategoryEditModal() {
    this.categoryEditModal.OpenCategoryEditModal();
  }

  OpenCategoryDeleteModal() {
    this.categoryDeleteModal.OpenCategoryDeleteModal();
  }
}
