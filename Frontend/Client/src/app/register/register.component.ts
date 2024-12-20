import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import {
  AfterViewInit,
  Component,
  ElementRef,
  inject,
  OnDestroy,
  OnInit,
  output,
  ViewChild,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  NgForm,
  Validators,
} from '@angular/forms';
import { catchError, tap, throwError } from 'rxjs';
import { TenantService } from '../_services/tenant.service';
import { Tenant } from '../_models/tenant';
import { AccountService } from '../_services/account.service';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { PasswordStrengthIndicatorComponent } from './password-strenght-indicator';

class UserComponent {
  password: string = '';
  showPassword: boolean = false;

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}

interface PasswordModel {
  password: string;
}

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    RouterLink,
    PasswordStrengthIndicatorComponent,
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  @ViewChild('passwordInput') passwordInput!: ElementRef;
  @ViewChild('fileInput') fileInput!: ElementRef;

  private accountService = inject(AccountService);
  cancelRegister = output<boolean>();
  registerForm: FormGroup;

  model: any = {
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    birthDate: '',
    address: '',
    discountId: null,
    tenantId: null,
    image: null,
  };

  private router = inject(Router);
  tenantService = inject(TenantService);
  private titleService = inject(Title);
  tenants: Tenant[] = []; // Array to hold tenant data
  errorMessage: string = '';
  showPassword: boolean = false;
  showPasswordStrength: boolean = false;
  hasUpperCase = false;
  hasLowerCase = false;
  hasNumber = false;
  hasSpecialChar = false;
  hasMinLength = false;

  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      birthDate: ['', Validators.required],
      address: ['', Validators.required],
      tenantId: [null, Validators.required],
      image: [null],
    });
  }
  ngOnInit(): void {
    this.titleService.setTitle('Register');
    this.fetchTenants();
  }

  fetchTenants(): void {
    this.tenantService.fetchTenants().subscribe({
      next: (data) => {
        if (Array.isArray(data)) {
          this.tenants = data;
        } else {
          console.error('Unexpected data format:', data);
          this.errorMessage = 'Received unexpected data format from server.';
        }
      },
      error: (error) => {
        console.error('Error fetching tenants:', error);
        this.errorMessage =
          error.message || 'Failed to load tenants. Please try again later.';
      },
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      const validImageTypes = ['image/jpeg', 'image/png', 'image/gif']; // Add allowed types

      if (!validImageTypes.includes(file.type)) {
        alert('Please upload a valid image file (JPEG, PNG, or GIF).');
        this.clearFileInput(); // Automatically clear the file input
        return;
      }
      const reader = new FileReader();
      reader.onload = () => {
        // Convert file to base64 string and store in model.image
        this.model.image = reader.result?.toString().split(',')[1]; // Base64 part
      };
      reader.readAsDataURL(file); // Read file as base64
    }
  }

  clearFileInput(): void {
    this.fileInput.nativeElement.value = ''; // Clear the file input value
    this.model.image = null; // Reset the model's image property
  }

  register(registerForm: NgForm) {
    if (registerForm.valid) {
      this.accountService.register(this.model).subscribe({
        next: (response) => {
          console.log('Registration successful:', response);
          this.router.navigateByUrl('/home');
          this.cancel();
        },
        error: (error) => {
          console.error('Registration failed:', error);
          if (error.status === 400) {
            this.errorMessage =
              'Invalid registration data. Please check your input.';
          } else {
            this.errorMessage =
              'An unexpected error occurred. Please try again later.';
          }
        },
      });
    } else {
      Object.keys(registerForm.controls).forEach((field) => {
        const control = registerForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
    }
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  checkPasswordStrength(): void {
    const password = this.model.password;
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
}
