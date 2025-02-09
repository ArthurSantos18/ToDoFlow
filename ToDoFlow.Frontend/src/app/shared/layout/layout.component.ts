import { Component, effect, inject, Injector, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {
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

  onLogout() {
    this.authService.logout()
    this.router.navigate(['login']);
  }
}
