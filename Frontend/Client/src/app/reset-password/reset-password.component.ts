import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent {
  resetPasswordForm: FormGroup;

  constructor(private fb: FormBuilder, private router: Router) {
    this.resetPasswordForm = this.fb.group({
      code: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
    });
  }
  private titleService = inject(Title);

  ngOnInit()
  {
    this.titleService.setTitle("Reset password");
  }

  onSubmit() {
    if (this.resetPasswordForm.valid) {
      const { code, password, confirmPassword } = this.resetPasswordForm.value;
      if (password !== confirmPassword) {
        alert('Passwords do not match!');
        return;
      }
      console.log(`Password reset with code: ${code}`);
      this.router.navigate(['/login']);
    }
  }
}