import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../../core/services/category/category.service';
import { CategoryReadDto } from '../../../models/category';
import { AuthService } from '../../../core/services/auth/auth.service';
import { CommonModule } from '@angular/common';
import { EnumService } from '../../../core/services/enum/enum.service';
import { TaskItemService } from '../../../core/services/task-item/task-item.service';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-task-item-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './task-item-create.component.html',
  styleUrl: './task-item-create.component.css'
})
export class TaskItemCreateComponent implements OnInit {
  userId: number | null = null;
  categoriesByUserId: CategoryReadDto[] = []
  priorities: {[key: number]: string} = {}
  errorMessage: string | null = null;

  taskItemCreateForm: FormGroup

  constructor(private categoryService: CategoryService, private authService: AuthService, private enumService: EnumService, private taskItemService: TaskItemService, private fb: FormBuilder, private router: Router) {
    this.taskItemCreateForm = this.fb.group({
      name: new FormControl('', [Validators.required]),
      categoryId: new FormControl('', [Validators.required]),
      priority: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required])
    })
  }

  ngOnInit(): void {
    this.userId = Number(this.authService.getSubFromToken());
    this.loadCategory(this.userId)
    this.loadPriorities()
  }

  loadCategory(userId: number): void {
    this.categoryService.getCategoryByUser(userId).subscribe({
      next: (response) => {
        if (response.success === false) {
          this.errorMessage = response.message;
        }
        else {
          this.errorMessage = null;
          this.categoriesByUserId = response.data
        }
      },
      error: (err) => {
        this.errorMessage = err;
      }
    })
  }

  loadPriorities(): void {
    this.enumService.getPriority().subscribe({
      next: (response) => {
        if (response.success === false) {
          this.errorMessage = response.message;
        }
        else {
          this.errorMessage = null;
          this.priorities = response.data
        }
      },
      error: (err) => {
        this.errorMessage = err;
      }
    });
  }

  createTaskItem(): void {
    this.taskItemService.createTaskItem(this.taskItemCreateForm.value).subscribe({
      next: (response) => {
        if (response.success === false) {
          this.errorMessage = response.message;
        }
        else {
          this.errorMessage = null;
          this.router.navigate(['taskitem']);
        }
      },
      error: (err) => {
        this.errorMessage = err;
      }
    });
  }
}
