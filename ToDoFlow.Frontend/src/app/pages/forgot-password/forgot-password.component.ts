import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../core/services/auth/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent {
  forgotPasswordForm: FormGroup
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private fb: FormBuilder, private router: Router) {
    this.forgotPasswordForm = this.fb.group({
      email: new FormControl('', [Validators.required, Validators.email])
    })
  }

  sendEmail() {
    if (this.forgotPasswordForm.valid) {
      this.authService.forgotPassword(this.forgotPasswordForm.value).subscribe({
        next: (response) => {
          if (response.success === false) {
            this.errorMessage = response.message
          }
          else {
            this.errorMessage = null
            this.router.navigate(['/login'])
          }
        },
        error: (err) => {
          this.errorMessage = err
        }
      });
    }
  }
}
