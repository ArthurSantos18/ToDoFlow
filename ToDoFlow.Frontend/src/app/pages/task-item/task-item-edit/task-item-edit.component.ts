import { Component, Input } from '@angular/core';
import { CategoryReadDto } from '../../../models/category';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoryService } from '../../../core/services/category/category.service';
import { AuthService } from '../../../core/services/auth/auth.service';
import { EnumService } from '../../../core/services/enum/enum.service';
import { TaskItemService } from '../../../core/services/task-item/task-item.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { TaskItemReadDto } from '../../../models/task-item';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-task-item-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './task-item-edit.component.html',
  styleUrl: './task-item-edit.component.css'
})
export class TaskItemEditComponent {
  @Input() taskItemDetails: TaskItemReadDto | null = null;

  selectedCategoryId: number = 1;
  userId!: number
  categoriesByUserId: CategoryReadDto[] = []
  taskItemUpdate!: TaskItemReadDto
  priorities!: {[key: number]: string}
  errorMessage: string | null = null

  taskItemUpdateForm: FormGroup

  constructor(private categoryService: CategoryService, private authService: AuthService, private enumService: EnumService, private taskItemService: TaskItemService, private fb: FormBuilder, private route: ActivatedRoute, private router: Router) {

    this.taskItemUpdateForm = this.fb.group({
      id: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required]),
      categoryId: new FormControl('', [Validators.required]),
      status: new FormControl('', [Validators.required]),
      priority: new FormControl('', [Validators.required]),
      description: new FormControl('', [Validators.required])
    })
  }

  ngOnInit(): void {
    this.userId = Number(this.authService.getSubFromToken());
    const taskItemId = Number(this.route.snapshot.paramMap.get('id'))

    this.forkData(taskItemId, this.userId)
  }

  forkData(taskItemId: number, userId: number) {
    forkJoin({
      taskItemResponse: this.taskItemService.getTaskItemById(taskItemId),
      categoriesResponse: this.categoryService.getCategoryByUser(userId),
      prioritiesResponse: this.enumService.getPriority()
    }).subscribe({
      next: (response) => {
        if (response.categoriesResponse.success === false) {
          this.errorMessage = response.categoriesResponse.message;
        }
        else if (response.prioritiesResponse.success === false) {
          this.errorMessage = response.prioritiesResponse.message;
        }
        else if (response.taskItemResponse.success === false) {
          this.errorMessage = response.taskItemResponse.message;
        }
        else {
          this.taskItemUpdate = response.taskItemResponse.data;
          this.categoriesByUserId = response.categoriesResponse.data;
          this.priorities = response.prioritiesResponse.data;
          this.checkAndInitializeForm();
        }
      },
      error: (err) => {
        this.errorMessage = err;
      }
    });
  }

  checkAndInitializeForm(): void {
      this.taskItemUpdateForm?.patchValue({
        id: this.taskItemUpdate.id,
        name: this.taskItemUpdate.name,
        categoryId: this.taskItemUpdate.categoryId,
        status: this.taskItemUpdate.status,
        priority: this.taskItemUpdate.priority,
        description: this.taskItemUpdate.description
      });
  }

  updateTaskItem(): void {
    this.taskItemService.updateTaskItem(this.taskItemUpdateForm.value).subscribe({
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
