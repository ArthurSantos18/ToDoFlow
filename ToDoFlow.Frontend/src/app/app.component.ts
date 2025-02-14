import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthService } from './core/services/auth/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    if (this.authService.getToken()) {
      this.authService.startTokenTimer()
      console.log("tem token");
    }
    else {
      console.log('não tem token');
    }
  }

  title = 'ToDoFlow.Frontend';
}
