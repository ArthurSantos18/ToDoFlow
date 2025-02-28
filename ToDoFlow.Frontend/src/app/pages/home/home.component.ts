import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth/auth.service';
import { UserService } from '../../core/services/user/user.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  userName: string | null = null;
  userId: number | null = null;
  errorMessage: string | null = null;

  constructor(private userService: UserService, private authService: AuthService) { }

  ngOnInit(): void {
    this.userId = Number(this.authService.getSubFromToken());
    this.LoadUser();
  }

  LoadUser() {
    this.userService.getUserById(this.userId!).subscribe({
      next: (response) => {
        if (response.success === false) {
          this.errorMessage = response.message;
        }
        else {
          this.errorMessage = null;
          this.userName = response.data.name
        }
      },
      error: (err) => {
        this.errorMessage = err
      }
    });

  }
}
