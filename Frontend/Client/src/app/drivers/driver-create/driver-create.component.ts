import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { AccountService } from '../../_services/account.service';
import { CommonModule } from '@angular/common';
import { PasswordStrengthIndicatorComponent } from "../../register/password-strenght-indicator";
import { Driver } from '../../_models/driver';
import { DriverService } from '../../_services/driver.service';
import { LettersNumbersValidatorDirective } from '../../validators/letters-numbers.validator';
import { LettersOnlyValidatorDirective } from '../../validators/only-letters.validator';
import { DateValidatorDirective } from '../../validators/date.validator';
import { LettersNumbersDashesValidatorDirective } from '../../validators/letters-numbers-dashes.validator';
import { firstValueFrom } from 'rxjs';
import { MyAppUserService } from '../../_services/my-app-user.service';


@Component({
  selector: 'app-driver-create',
  standalone: true,
  imports: [FormsModule, DateValidatorDirective, LettersNumbersDashesValidatorDirective, LettersNumbersValidatorDirective, LettersOnlyValidatorDirective, CommonModule, RouterLink, PasswordStrengthIndicatorComponent],
  templateUrl: './driver-create.component.html',
  styleUrl: './driver-create.component.css',
})
export class DriverCreateComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;


  private driverService = inject(DriverService);
  private myAppUserService = inject(MyAppUserService);
  private router = inject(Router);
  private accountService = inject(AccountService);
  private titleService = inject(Title);
  drivers: Driver[] = [];
  errorMessage: string = '';
  registerForm: FormGroup;
  showPassword: boolean = false;
  showPasswordStrength: boolean = false;
  hasUpperCase = false;
  hasLowerCase = false;
  hasNumber = false;
  hasSpecialChar = false;
  hasMinLength = false;
  emailExists: boolean = false;
  maxDate: string = new Date().toISOString().split('T')[0];


  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }



  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      birthDate: ['', Validators.required],
      hireDate: ['', Validators.required],
      address: ['', Validators.required],
      department: [null, Validators.required],
      managerLevel: [null, Validators.required],
      image: [null],
    });
  }
  ngOnInit() {
    this.titleService.setTitle('Add driver');
    this.loadExistingDrivers();
  }

  loadExistingDrivers() {
    this.driverService.getAllDrivers().subscribe({
      next: (drivers) => {
        this.drivers = drivers;
      },
      error: (error) => {
        console.error('Error loading drivers:', error);
      }
    });
  }

  driverCreate: any = {};
  async addNewDriver(newDriverForm: NgForm) {
    if (newDriverForm.valid && !this.emailExists) {
      if (this.driverCreate.isActive === undefined) {
        this.driverCreate.isActive = false;
      }
      try{
      await firstValueFrom(this.accountService.registerDriver(this.driverCreate));
      this.cancel();
      }
      catch(err){
        console.error(err);
      }

      
    }
    else {
      Object.keys(newDriverForm.controls).forEach(field => {
        const control = newDriverForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
    }
  }
  cancel() {
    this.router.navigate(['/manager-dashboard/drivers']);
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
        this.driverCreate.image = reader.result?.toString().split(',')[1]; // Base64 part
      };
      reader.readAsDataURL(file); // Read file as base64
    }
  }

  clearFileInput(): void {
    this.fileInput.nativeElement.value = ''; // Clear the file input value
    this.driverCreate.image = null; // Reset the model's image property
  }

  checkPasswordStrength(): void {
    const password = this.driverCreate.password;
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

  checkEmailExists(): void {
    if (this.driverCreate.email) {
      this.myAppUserService.checkEmailExists(this.driverCreate.email).subscribe(response => {
        this.emailExists = response.exists;
      });
    }
  }
}
