import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { Manager } from '../../_models/manager';
import { AccountService } from '../../_services/account.service';
import { CommonModule } from '@angular/common';
import { Department } from '../../_models/department';
import { ManagerLevel } from '../../_models/manager-level';
import { PasswordStrengthIndicatorComponent } from "../../register/password-strenght-indicator";
import { ManagerService } from '../../_services/manager.service';


@Component({
  selector: 'app-manager-create',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterLink, PasswordStrengthIndicatorComponent],
  templateUrl: './manager-create.component.html',
  styleUrl: './manager-create.component.css',
})
export class ManagerCreateComponent {
    @ViewChild('fileInput') fileInput!: ElementRef;
  private managerService = inject(ManagerService);
  private router = inject(Router);
  private accountService= inject(AccountService);
  private titleService = inject(Title);
  managers: Manager[] = [];
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
  maxDate: string;

  managerLevels: ManagerLevel[] = [
    { id: 1, name: 'Junior Manager' },
    { id: 2, name: 'Mid-Level Manager' },
    { id: 3, name: 'Senior Manager' }
  ];

  departments: Department[] = [
    { id: 1, name: 'Management' },
    { id: 2, name: 'Human Resources' },
    { id: 3, name: 'Finance' },
    { id: 4, name: 'IT' },
    { id: 5, name: 'Marketing' }
  ];
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
    this.maxDate = new Date().toISOString().split('T')[0];
  }
  ngOnInit() {
    this.titleService.setTitle('Add manager');
    this.loadExistingManagers();
  }

  loadExistingManagers() {
    this.managerService.getAllManagers().subscribe({
      next: (managers) => {
        this.managers = managers;
      },
      error: (error) => {
        console.error('Error loading managers:', error);
      }
    });
  }

  managerCreate: any = {};
  addNewManager(newManagerForm: NgForm) {

    if (newManagerForm.valid && !this.emailExists) {
      if (this.managerCreate.isActive === undefined) {
        this.managerCreate.isActive = false;
      }
      console.log(this.managerCreate);
      this.accountService.registerManager(this.managerCreate).subscribe({
        next: (response) => {
          console.log(response);
          this.cancel();
        },  
      });
      this.router.navigate(['/manager-dashboard/managers']);
    }
    else{
      Object.keys(newManagerForm.controls).forEach(field => {
        const control = newManagerForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
    }
  }
  cancel() {
    this.router.navigate(['/manager-dashboard/managers']);
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
        this.managerCreate.image = reader.result?.toString().split(',')[1]; // Base64 part
      };
      reader.readAsDataURL(file); // Read file as base64
    }
  }

  clearFileInput(): void {
    this.fileInput.nativeElement.value = ''; // Clear the file input value
    this.managerCreate.image = null; // Reset the model's image property
  }
  
  checkPasswordStrength(): void {
    const password = this.managerCreate.password;
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

  checkEmailExists() {
    if (this.managerCreate.email) {
      this.emailExists = this.managers.some(manager => manager.email === this.managerCreate.email);
    }
  }
}
