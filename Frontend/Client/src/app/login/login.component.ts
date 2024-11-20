import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="container">
      <div class="row justify-content-center mt-5">
        <div class="col-md-6">
          <div class="card">
            <div class="card-body">
              <h2 class="card-title text-center mb-4">Sign in to your account</h2>
              <form [formGroup]="loginForm" (ngSubmit)="onSubmit()">
                <div class="mb-3">
                  <label for="email-address" class="form-label">Email address</label>
                  <input id="email-address" type="email" formControlName="email" class="form-control" required placeholder="Email address" />
                </div>
                <div class="mb-3">
                  <label for="password" class="form-label">Password</label>
                  <input id="password" type="password" formControlName="password" class="form-control" required placeholder="Password" />
                </div>
                <div class="d-grid">
                  <button type="submit" [disabled]="!loginForm.valid" class="btn btn-primary">
                    Sign in
                  </button>
                </div>
              </form>
              <div *ngIf="error" class="alert alert-danger mt-3" role="alert">
                {{ error }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: []
})
export class LoginComponent {
  loginForm: FormGroup;
  error: string = '';

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
      this.authService.login(email, password).subscribe({
        next: (success) => {
          if (success) {
            console.log('Login successful');
            // Navigate to dashboard or home page
          } else {
            this.error = 'Invalid credentials';
          }
        },
        error: (error) => {
          this.error = 'An error occurred. Please try again.';
          console.error(error);
        }
      });
    }
  }
}