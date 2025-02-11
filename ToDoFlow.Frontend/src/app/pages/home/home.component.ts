import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth/auth.service';
import { UserService } from '../../core/services/user/user.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  userName: string | null = null;
  userId: number | null = null;

  constructor(private userService: UserService, private authService: AuthService) { }

  ngOnInit(): void {
    this.userId = Number(this.authService.getSubFromToken());
    this.LoadUser();
  }

  LoadUser() {
    this.userService.getUserById(this.userId!).subscribe((response) => {
      this.userName = response.data['name']
    })
  }
}
