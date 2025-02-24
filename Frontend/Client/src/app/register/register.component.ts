import { HttpClient, HttpClientModule, HttpErrorResponse } from '@angular/common/http';
import {AfterViewInit,Component,ElementRef,inject,OnDestroy,OnInit,output,ViewChild,} from '@angular/core';
import {FormBuilder,FormGroup,FormsModule,NgForm,ReactiveFormsModule,Validators,} from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { BrowserModule, Title } from '@angular/platform-browser';
import { PasswordStrengthIndicatorComponent } from './password-strenght-indicator';
import { NgxCaptchaModule, ReCaptcha2Component } from 'ngx-captcha';

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
    ReactiveFormsModule,
    PasswordStrengthIndicatorComponent,
    NgxCaptchaModule

  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  @ViewChild('passwordInput') passwordInput!: ElementRef;
  @ViewChild('fileInput') fileInput!: ElementRef;
  @ViewChild('captchaElem') captchaElem: any;


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
    image: null,
  };

  private router = inject(Router);
  private titleService = inject(Title);
  errorMessage: string = '';
  showPassword: boolean = false;
  showPasswordStrength: boolean = false;
  hasUpperCase = false;
  hasLowerCase = false;
  hasNumber = false;
  hasSpecialChar = false;
  hasMinLength = false;
  captchaResponse: string | null = null;
  maxDate: string = new Date().toISOString().split('T')[0];

  siteKey: string = '6LfSlaEqAAAAACLF8vRUOkumCmiEBrQrkAG6fQLb';

  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      birthDate: ['', Validators.required],
      address: ['', Validators.required],
      image: [null],
    });
  }
  ngOnInit(): void {
    this.titleService.setTitle('Register');
  
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
    console.log(this.registerForm);
    if (registerForm.valid && this.captchaResponse) {
      const formData = { ...this.model, captchaResponse: this.captchaResponse };
      this.accountService.register(formData).subscribe({
        next: (response) => {
          console.log('Registration successful:', response);
          this.router.navigateByUrl('/home').then(() => {
            window.location.reload(); 
          });
          this.cancel();
        },
        error: (error) => {
          console.log(formData);
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
      this.registerForm.markAllAsTouched();
      if (!this.captchaResponse) {
        this.errorMessage = 'Please complete the CAPTCHA';
      }
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

  handleSuccess(captchaResponse: string): void {
    this.captchaResponse = captchaResponse;
  }

  handleLoad(): void {
    console.log('reCAPTCHA loaded');
  }

  handleExpire(): void {
    this.captchaResponse = null;
    console.log('reCAPTCHA expired');
  }
  handleReset(): void {
    this.captchaResponse = null;
    console.log('reCAPTCHA reset');
  }
}
