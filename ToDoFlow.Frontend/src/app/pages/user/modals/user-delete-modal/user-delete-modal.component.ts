import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { UserService } from '../../../../core/services/user/user.service';

declare const bootstrap: any

@Component({
  selector: 'app-user-delete-modal',
  standalone: true,
  imports: [],
  templateUrl: './user-delete-modal.component.html',
  styleUrl: './user-delete-modal.component.css'
})
export class UserDeleteModalComponent {
  @ViewChild('IuserDeleteModal') modalElement!: ElementRef

  @Input() userName: string | null = null
  @Input() userId: number | null = null

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
      next: () => {
        location.reload()
      },
      error: (error) => console.error('')
    });
  }
}
