import { Component } from '@angular/core';
import { UserReadDto } from '../../../models/user';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../core/services/auth/auth.service';
import { UserService } from '../../../core/services/user/user.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile-reset-password',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './profile-reset-password.component.html',
  styleUrl: './profile-reset-password.component.css'
})
export class ProfileResetPasswordComponent {
  userId: number | null = null;
  userReadDto: UserReadDto | null = null;
  userRole: string | null = null;
  errorMessage: string | null = null;

  userUpdateForm: FormGroup


  constructor(private authService: AuthService, private userService: UserService, private fb: FormBuilder, private router: Router) {
    this.userUpdateForm = fb.group({
      id: new FormControl(''),
      name: new FormControl(''),
      email: new FormControl(''),
      profile: new FormControl(''),
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required])
    },
    {
      validator: this.passwordsMatch
    });
  }

  ngOnInit(): void {
    this.userId = Number(this.authService.getSubFromToken());
    this.loadUser();
  }

  passwordsMatch(group: FormGroup) {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { notMatching: true };
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
    this.userUpdateForm.patchValue({
      id: this.userId,
      name: this.userReadDto?.name,
      email: this.userReadDto?.email,
      profile: this.authService.getRoleFromToken()
    });

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
