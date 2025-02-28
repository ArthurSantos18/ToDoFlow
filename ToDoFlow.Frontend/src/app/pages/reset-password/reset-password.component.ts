import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, CommonModule],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm: FormGroup;
  email: string | null = null;
  token: string | null = null;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private authService: AuthService) {
    this.resetPasswordForm = this.fb.group({
      email: new FormControl('', [Validators.required, Validators.email]),
      token: new FormControl('', [Validators.required]),
      newPassword: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required]),
    },
    {
      validator: this.passwordsMatch
    });
  }

  ngOnInit() {
    this.email = this.route.snapshot.queryParamMap.get('email') || '';
    this.token = this.route.snapshot.queryParamMap.get('token') || '';

    if (this.email == '' || this.token == '' || this.authService.isTokenExpired(this.token)) {
      this.router.navigate(['login']);
    }

    this.resetPasswordForm.patchValue({
      email: this.email,
      token: this.token
    });
  }

  passwordsMatch(group: FormGroup) {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;
    return password === confirmPassword ? null : { notMatching: true };
  }

  resetPassword() {
    if (this.resetPasswordForm.valid) {
      this.authService.resetPassword(this.resetPasswordForm.value).subscribe({
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
