import { Component, OnInit, ViewChild } from '@angular/core';
import { CategoryCreateModalComponent } from './modals/category-create-modal/category-create-modal.component';
import { CategoryEditModalComponent } from './modals/category-edit-modal/category-edit-modal.component';
import { CategoryDeleteModalComponent } from './modals/category-delete-modal/category-delete-modal.component';
import { CategoryService } from '../../core/services/category/category.service';
import { AuthService } from '../../core/services/auth/auth.service';
import { CommonModule } from '@angular/common';
import { CategoryReadDto } from '../../models/category';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [CategoryCreateModalComponent, CategoryEditModalComponent, CategoryDeleteModalComponent, CommonModule],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent implements OnInit {
  @ViewChild (CategoryCreateModalComponent) categoryCreateModal!: CategoryCreateModalComponent
  @ViewChild (CategoryEditModalComponent) categoryEditModal!: CategoryEditModalComponent
  @ViewChild (CategoryDeleteModalComponent) categoryDeleteModal!: CategoryDeleteModalComponent

  userId: number | null = null;
  errorMessage: string | null = null;
  categoriesByUserId: CategoryReadDto[] = []

  constructor(private categoryService: CategoryService, private authService: AuthService) { }

  ngOnInit(): void {
    this.userId = Number(this.authService.getSubFromToken());
    this.loadCategory(this.userId)
  };

  loadCategory(userId: number): void {
    this.categoryService.getCategoryByUser(userId).subscribe({
      next: (response) => {
        if (response.success === false) {
          this.errorMessage = response.message;
        }
        else {
          this.categoriesByUserId = response.data;
          this.errorMessage = null;
        }
      },
      error: (err) => {
        this.errorMessage = err;
      }
    });
  }

  openCategoryCreateModal(): void {
    this.categoryCreateModal.openCategoryCreateModal();
  }

  openCategoryEditModal(categoryId: number, categoryName: string): void {
    this.categoryEditModal.categoryId = categoryId;
    this.categoryEditModal.categoryName = categoryName;
    this.categoryEditModal.openCategoryEditModal();
  }

  openCategoryDeleteModal(categoryId: number, categoryName: string): void {
    this.categoryDeleteModal.categoryId = categoryId
    this.categoryDeleteModal.categoryName = categoryName
    this.categoryDeleteModal.openCategoryDeleteModal();
  }
}
