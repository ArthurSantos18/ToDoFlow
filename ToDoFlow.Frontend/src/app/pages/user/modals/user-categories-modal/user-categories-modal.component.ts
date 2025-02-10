import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { CategoryReadDto } from '../../../../models/category';
import { CommonModule } from '@angular/common';

declare const bootstrap: any

@Component({
  selector: 'app-user-categories-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-categories-modal.component.html',
  styleUrl: './user-categories-modal.component.css'
})
export class UserCategoriesModalComponent {
  @ViewChild("IUserCategoriesModal") modalElement!: ElementRef

  @Input() userName: string | null = null;
  @Input() categoriesByUserId: CategoryReadDto[] = []

  modal: any

  openUserCategoriesModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal = new bootstrap.Modal(this.modalElement.nativeElement)
      this.modal.show()
    }
    else {
      console.error('Modal element not found!');
    }
  }

  closeUserCategoriesModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal.hide()
    }
    else {
      console.error('Modal Element not found!')
    }
  }
}
