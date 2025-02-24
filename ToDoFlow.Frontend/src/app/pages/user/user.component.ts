import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../core/services/user/user.service';
import { UserReadDto } from '../../models/user';
import { UserCategoriesModalComponent } from './modals/user-categories-modal/user-categories-modal.component';
import { TaskItemService } from '../../core/services/task-item/task-item.service';
import { CategoryService } from '../../core/services/category/category.service';
import { UserTaskItemsModalComponent } from './modals/user-task-items-modal/user-task-items-modal.component';
import { UserDeleteModalComponent } from "./modals/user-delete-modal/user-delete-modal.component";

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule, UserCategoriesModalComponent, UserTaskItemsModalComponent, UserDeleteModalComponent],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent implements OnInit {
  @ViewChild(UserCategoriesModalComponent) userCategoriesModal!: UserCategoriesModalComponent;
  @ViewChild(UserTaskItemsModalComponent) userTaskItemsModal!: UserTaskItemsModalComponent;
  @ViewChild(UserDeleteModalComponent) userDeleteModal!: UserDeleteModalComponent;

  Users: UserReadDto[] = []

  constructor(private userService: UserService, private categoryService: CategoryService, private taskItemService: TaskItemService) {}

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser() {
    this.userService.getAllUsers().subscribe((response) => {
      this.Users = response.data.filter(user => user.profile === 'Default')
    })
  }

  openUserCategoriesModal(userId: number, userName: string): void {
    this.categoryService.getCategoryByUser(userId).subscribe((response) => {
      this.userCategoriesModal.categoriesByUserId = response.data;
      this.userCategoriesModal.userName = userName;
    })
    this.userCategoriesModal.openUserCategoriesModal();
  }

  openUserTaskItemsModal(userId: number, userName: string): void {
    this.taskItemService.getTaskItemByUser(userId).subscribe((response) => {
      this.userTaskItemsModal.taskItemsByUserId = response.data;
      this.userTaskItemsModal.userName = userName
    })
    this.userTaskItemsModal.openUserTaskItemModal();
  }

  openUserDeleteModal(userId: number, userName: string): void {
    this.userDeleteModal.userId = userId
    this.userDeleteModal.userName = userName
    this.userDeleteModal.openUserDeleteModal()
  }
}
