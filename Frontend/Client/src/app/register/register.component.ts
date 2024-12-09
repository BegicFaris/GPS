import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, inject, Input, input, output, Output } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, Validators } from '@angular/forms';
import { catchError, tap, throwError } from 'rxjs';
import { TenantService } from '../_services/tenant.service';
import { Tenant } from '../_models/tenant';
import { AccountService } from '../_services/account.service';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { Title } from '@angular/platform-browser';
class UserComponent {
  password: string = '';
  showPassword: boolean = false;

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})



export class RegisterComponent {


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

  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(8),
          Validators.pattern(/(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+/),
        ],
      ],
      birthDate: ['', Validators.required],
      address: ['', Validators.required],
      tenantId: [null, Validators.required],
      image: [null],
    });
  }
  ngOnInit(): void {
    this.titleService.setTitle("Register");
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
        this.errorMessage = error.message || 'Failed to load tenants. Please try again later.';
      },
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input?.files?.length) {
      const file = input.files[0];
      const allowedExtensions = ['image/jpeg', 'image/png', 'image/gif']; // Add allowed image types here

      if (!allowedExtensions.includes(file.type)) {
        this.errorMessage = 'Please upload a valid image file (JPEG, PNG, GIF).';
        this.model.image = null;
        return;
      }

      const reader = new FileReader();
      reader.onload = () => {
        if (reader.result) {
          const base64String = btoa(
            String.fromCharCode(...new Uint8Array(reader.result as ArrayBuffer))
          );
          this.model.image = base64String; // Set image as base64 string
        } else {
          this.errorMessage = 'Failed to read the file.';
          this.model.image = null;
        } //Convert to array for JSON serialization
      };
      reader.onerror = () => {
        this.errorMessage = 'Failed to read the file. Please try again.';
        this.model.image = null;
      };
      reader.readAsArrayBuffer(file);
    } else {
      this.errorMessage = 'No file selected.';
      this.model.image = null;
    }
  }

  register(registerForm: NgForm) {

    if (registerForm.valid) {
      this.accountService.register(this.model).subscribe({
        next: response => {
          console.log('Registration successful:', response);
          this.router.navigateByUrl('/home');
          this.cancel();
        },
        error: error => {
          console.error('Registration failed:', error);
          if (error.status === 400) {
            this.errorMessage = 'Invalid registration data. Please check your input.';
          } else {
            this.errorMessage = 'An unexpected error occurred. Please try again later.';
          }
        }
      });
    }
    else {
      Object.keys(registerForm.controls).forEach(field => {
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
}
