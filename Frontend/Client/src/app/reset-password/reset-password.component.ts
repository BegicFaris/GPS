import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { PasswordResetService } from '../_services/password-reset.service';
import { CommonModule } from '@angular/common';
import { PasswordStrengthIndicatorComponent } from "../register/password-strenght-indicator";

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, FormsModule, PasswordStrengthIndicatorComponent],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent {
  email: string = '';
  verificationCode: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  codeSent: boolean = false;
  codeVerified: boolean = false;
  private router = inject(Router);

  // Validation messages
  emailError: string = '';
  codeError: string = '';
  passwordError: string = '';
  confirmPasswordError: string = '';

  constructor(private passwordResetService: PasswordResetService) {}

  showPasswordStrength: boolean = false;
  hasUpperCase = false;
  hasLowerCase = false;
  hasNumber = false;
  hasSpecialChar = false;
  hasMinLength = false;

  sendResetCode() {
    if (!this.validateEmail()) {
      return;
    }
    this.passwordResetService.sendResetCode(this.email).subscribe(
      () => {
        this.codeSent = true;
        this.emailError = '';
      },
      (error: any) => {
        console.error('Error sending reset code', error);
        this.emailError = 'Email not found or invalid.';
      }
    );
  }

  verifyCode() {
    if (!this.validateCode()) {
      return;
    }
    this.passwordResetService.verifyCode(this.email, this.verificationCode).subscribe(
      () => {
        this.codeVerified = true;
        this.codeError = '';
      },
      (error: any) => {
        console.error('Error verifying code', error);
        this.codeError = 'Invalid verification code.';
      }
    );
  }

  resetPassword() {
    if (!this.validatePasswords()) {
      return;
    }
    this.passwordResetService.resetPassword(this.email, this.verificationCode, this.newPassword).subscribe(
      () => {
        this.router.navigate(['/home']);
      },
      (error: any) => {
        this.passwordError = 'Error resetting password. Please try again.';
      }
    );
  }

  checkPasswordStrength(): void {
    const password = this.newPassword;
    this.hasUpperCase = /[A-Z]/.test(password);
    this.hasLowerCase = /[a-z]/.test(password);
    this.hasNumber = /\d/.test(password);
    this.hasSpecialChar = /[$!%_*?&]/.test(password);
    this.hasMinLength = password.length >= 8;
  }

  onPasswordInput(): void {
    this.checkPasswordStrength();
  }

  onPasswordFocus(): void {
    this.showPasswordStrength = true;
  }

  onPasswordBlur(): void {
    setTimeout(() => {
      this.showPasswordStrength = false;
    }, 200);
  }

  validateEmail(): boolean {
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!emailRegex.test(this.email)) {
      this.emailError = 'Please enter a valid email address.';
      return false;
    }
    this.emailError = '';
    return true;
  }

  validateCode(): boolean {
    if (this.verificationCode.length !== 6 || !/^\d+$/.test(this.verificationCode)) {
      this.codeError = 'Please enter a valid 6-digit code.';
      return false;
    }
    this.codeError = '';
    return true;
  }

  validatePasswords(): boolean {
    if (!this.hasUpperCase || !this.hasLowerCase || !this.hasNumber || !this.hasSpecialChar || !this.hasMinLength) {
      this.passwordError = 'Password does not meet the strength requirements.';
      return false;
    }
    if (this.newPassword !== this.confirmPassword) {
      this.confirmPasswordError = 'Passwords do not match.';
      return false;
    }
    this.passwordError = '';
    this.confirmPasswordError = '';
    return true;
  }

  cancel() {
    this.router.navigate(["/home"]);
  }
}

