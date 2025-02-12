import { Component, ElementRef, ViewChild } from '@angular/core';
import { AuthService } from '../../../core/services/auth/auth.service';
import { Router } from '@angular/router';

declare const bootstrap: any

@Component({
  selector: 'app-layout-logout-modal',
  standalone: true,
  imports: [],
  templateUrl: './layout-logout-modal.component.html',
  styleUrl: './layout-logout-modal.component.css'
})
export class LayoutLogoutModalComponent {
  @ViewChild('IlayoutLogoutModal') modalElement!: ElementRef

  modal: any

  constructor(private authService: AuthService, private router: Router){ }

  openLayoutLogoutModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal = new bootstrap.Modal(this.modalElement.nativeElement);
      this.modal.show();
    }
    else {
      console.error('Modal element not found!');
    }
  }

  closeLayoutLogoutModal(): void {
    if (this.modalElement && this.modalElement.nativeElement) {
      this.modal.hide();
    }
    else {
      console.error('Modal element not found!')
    }
  }

  onLogout() {
    this.authService.logout();
    this.closeLayoutLogoutModal();
    this.router.navigate(['login']);
  }
}
