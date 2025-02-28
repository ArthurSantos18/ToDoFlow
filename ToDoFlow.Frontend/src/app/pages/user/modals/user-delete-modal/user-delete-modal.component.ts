import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { UserService } from '../../../../core/services/user/user.service';
import { CommonModule } from '@angular/common';

declare const bootstrap: any

@Component({
  selector: 'app-user-delete-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-delete-modal.component.html',
  styleUrl: './user-delete-modal.component.css'
})
export class UserDeleteModalComponent {
  @ViewChild('IuserDeleteModal') modalElement!: ElementRef

  @Input() userName: string | null = null
  @Input() userId: number | null = null

  errorMessage: string | null = null
  modal: any

  constructor(private userService: UserService) { }

  openUserDeleteModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal = new bootstrap.Modal(this.modalElement.nativeElement);
      this.modal.show();
    }
    else {
      console.error('Modal Element not found!')
    }
  }

  closeUserDeleteModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal.hide()
    }
    else {
      console.error('Modal Element not found!')
    }
  }

  deleteUser(): void {
    this.userService.deleteUser(this.userId!).subscribe({
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
        this.errorMessage = err;
      }
    });
  }
}
