import { Component, effect, inject, Injector, OnInit, ViewChild } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth/auth.service';
import { CommonModule } from '@angular/common';
import { LayoutLogoutModalComponent } from './layout-logout-modal/layout-logout-modal.component';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterModule, CommonModule, LayoutLogoutModalComponent],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {
  @ViewChild(LayoutLogoutModalComponent) layoutLogoutModal!: LayoutLogoutModalComponent

  isLoggedIn: Boolean = false
  userRole: string | null = null

  injector = inject(Injector)

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    effect(() => {
      this.isLoggedIn = this.authService.isLoggedIn()
      this.userRole = this.authService.getRoleFromToken()
    },
    {injector: this.injector})
  }

  openLayoutLogoutModal(): void {
    this.layoutLogoutModal.openLayoutLogoutModal()
  }

}
