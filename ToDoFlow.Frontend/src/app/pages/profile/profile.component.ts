import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../core/services/auth/auth.service';
import { CommonModule } from '@angular/common';
import { UserReadDto } from '../../models/user';
import { UserService } from '../../core/services/user/user.service';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  userId: number | null = null;
  userReadDto: UserReadDto | null = null
  userRole: string | null = null
  errorMessage: string | null = null;

  userUpdateForm: FormGroup


  constructor(private authService: AuthService, private userService: UserService, private fb: FormBuilder, private router: Router) {
    this.userUpdateForm = fb.group({
      id: new FormControl(0, [Validators.required]),
      name: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.email , Validators.required]),
      profile: new FormControl('', [Validators.required])
    });
  }

  ngOnInit(): void {
    this.userId = Number(this.authService.getSubFromToken());
    this.loadUser();

    this.userUpdateForm.patchValue({
      id: this.userId,
      profile: this.authService.getRoleFromToken()
    });

  }

  loadUser(): void {
    this.userService.getUserById(this.userId!).subscribe({
      next: (response) => {
        if (response.success == false) {
          this.errorMessage = response.message
        }
        else {
          this.userReadDto = response.data
          this.errorMessage = null
        }
      },
      error: (err) => {
        this.errorMessage = err
      }
    });
  }

  updateUser(): void {
    const name = this.userUpdateForm.get('name')?.value.trim();
    const email = this.userUpdateForm.get('email')?.value.trim();

    if (name == '') {
      this.userUpdateForm.patchValue({
        name: this.userReadDto?.name,
      });
    }

    if (email == '') {
      this.userUpdateForm.patchValue({
        email: this.userReadDto?.email
      });
    }

    if(this.userUpdateForm.valid) {
      this.userService.updateUser(this.userUpdateForm.value).subscribe({
        next: (response) => {
          if (response.success === false) {
            this.errorMessage = response.message
          }
          else {
            this.errorMessage = null
            this.router.navigate(['/home'])
          }
        },
        error: (err) => {
          this.errorMessage = err
        }
      });
    }
  }
}
