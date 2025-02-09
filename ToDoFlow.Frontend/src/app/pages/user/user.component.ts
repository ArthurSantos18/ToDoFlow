import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../core/services/user/user.service';
import { UserReadDto } from '../../models/user';
import { UserCategoriesModalComponent } from './modals/user-categories-modal/user-categories-modal.component';
import { TaskItemService } from '../../core/services/task-item/task-item.service';
import { CategoryService } from '../../core/services/category/category.service';
import { UserTaskItemsModalComponent } from './modals/user-task-items-modal/user-task-items-modal.component';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule, UserCategoriesModalComponent, UserTaskItemsModalComponent],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit {
  @ViewChild(UserCategoriesModalComponent) userCategoriesModal!: UserCategoriesModalComponent;
  @ViewChild(UserTaskItemsModalComponent) userTaskItemsModal!: UserTaskItemsModalComponent;

  Users: UserReadDto[] = []

  constructor(private userService: UserService, private categoryService: CategoryService, private taskItemService: TaskItemService) {}

  ngOnInit(): void {
    this.LoadUser();
  }

  LoadUser() {
    this.userService.getAllUsers().subscribe((response) => {
      this.Users = response.data
    })
  }

  openUserCategoriesModal(userId: number, userName: string) {
    this.categoryService.getCategoryByUser(userId).subscribe((response) => {
      this.userCategoriesModal.categoriesByUserId = response.data;
      this.userCategoriesModal.userName = userName;
    })
    this.userCategoriesModal.openUserCategoriesModal();
  }

  openTaskItemsModal(userId: number, userName: string) {
    this.taskItemService.getTaskItemByUser(userId).subscribe((response) => {
      this.userTaskItemsModal.taskItemsByUserId = response.data;
      this.userTaskItemsModal.userName = userName
    })
    this.userTaskItemsModal.openUserTaskItemModal();
  }
}
