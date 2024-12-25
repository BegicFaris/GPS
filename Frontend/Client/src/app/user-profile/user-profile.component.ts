import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, AbstractControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { MyAppUserService } from '../_services/my-app-user.service';
import { TicketService } from '../_services/ticket.service';
import { AccountService } from '../_services/account.service';
import { MatDialog } from '@angular/material/dialog';
import { QrCodeDialogComponent } from '../qr-code-dialog/qr-code-dialog.component';
import { PassengerService } from '../_services/passenger.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ManagerService } from '../_services/manager.service';
import { DriverService } from '../_services/driver.service';
import { debounceTime, first, Observable, of, switchMap } from 'rxjs';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTableModule
  ]
})
export class UserProfileComponent implements OnInit {
  userProfile: any;
  profileForm!: FormGroup;
  tickets: any[] = [];
  userType: string = '';
  profileImageUrl: SafeUrl | null = null;
  hasTickets= false;
  originalEmail: string = '';
  maxDate: string = new Date().toISOString().split('T')[0];

  constructor(
    private userService: MyAppUserService,
    private accountService: AccountService,
    private ticketService: TicketService,
    private passengerService: PassengerService,
    private managerService: ManagerService,
    private driverService: DriverService,
    private fb: FormBuilder,
    private sanitizer: DomSanitizer,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit() {
    this.initForm();
    this.loadUserProfile();
    this.loadUserTickets();
  }

  initForm() {
    this.profileForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email], [this.emailExistsValidator()]],
      birthDate: ['', Validators.required],
      address: ['', Validators.required],
      twoFactorEnabled: [false],
    });
  }

  emailExistsValidator(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      if (control.value === this.originalEmail) {
        return of(null);
      }
      return this.userService.checkEmailExists(control.value).pipe(
        debounceTime(300),
        switchMap(response => {
          return response.exists ? of({ emailExists: true }) : of(null);
        }),
        first()
      );
    };
  }

  loadUserProfile() {
    const email = this.accountService.getUserEmail();
    if (email) {
      this.userService.getMyAppUserByEmail(email).subscribe({
        next: (user) => {
          this.userProfile = user;
          console.log(user);
          console.log(this.userProfile)
          this.userType = this.getUserType(user); 
          this.originalEmail = user.email;
          this.updateForm();
          this.updateProfileImage();
        },
        error: (error) => {
          console.error('Error loading user profile:', error);
        }
      });
    }
  }

  loadUserTickets() {
    const email = this.accountService.getUserEmail();
    if (email) {
      this.ticketService.getUserTickets(email).subscribe({
        next: (tickets) => {
          if (tickets && tickets.length > 0) {
          this.tickets = tickets.map(ticket => ({
            ...ticket,
            qrCode: ticket.qrCode ? this.decodeQrCode(ticket.qrCode) : null
          }));
          this.hasTickets = true;
          } else {
            this.tickets = [];
            this.hasTickets = false;
          }
        },
        error: (error) => {
          this.tickets = [];
          this.hasTickets=false; 
          if (error.status === 404) {
            // Default to an empty array
          } else {
            console.error('Failed to load tickets:', error);
          }
        },
      });
    }
  }



  updateForm() {
    this.profileForm.patchValue({
      firstName: this.userProfile.firstName,
      lastName: this.userProfile.lastName,
      email: this.userProfile.email,
      birthDate: this.userProfile.birthDate,
      address: this.userProfile.address,
      twoFactorEnabled: this.userProfile.twoFactorEnabled,
    });
  }

  updateProfileImage() {
    if (this.userProfile && this.userProfile.image) {
      let imageUrl: string;
      
      if (typeof this.userProfile.image === 'string') {
        // If it's a string, it's either a base64 string or a data URL
        imageUrl = this.userProfile.image.startsWith('data:') 
          ? this.userProfile.image 
          : `data:image/jpeg;base64,${this.userProfile.image}`;
      } else if (this.userProfile.image instanceof Uint8Array) {
        // If it's a Uint8Array, convert it to a base64 string
        imageUrl = `data:image/jpeg;base64,${this.arrayBufferToBase64(this.userProfile.image.buffer)}`;
      } else {
        this.profileImageUrl = null;
        return;
      }
      this.profileImageUrl = this.sanitizer.bypassSecurityTrustUrl(imageUrl);
    } else {
      this.profileImageUrl = null;
    }
  }
  handleImageError(event: any) {
    console.error('Error loading image:', event);
    event.target.src = 'default-profile-image.jpg'; // Set a default image
  }
  
  handleImageLoad(event: any) {
    console.log('Image loaded successfully:', event);
  }
  // Helper method to convert ArrayBuffer to base64
  arrayBufferToBase64(buffer: ArrayBuffer): string {
    let binary = '';
    const bytes = new Uint8Array(buffer);
    const len = bytes.byteLength;
    for (let i = 0; i < len; i++) {
      binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
  }

  onSubmit() {
    if (this.profileForm.valid) {
      const email = this.accountService.getUserEmail();
      if (email) {
        const updatedUser = {
          ...this.profileForm.value,
          id: this.userProfile.id,
          registrationDate: this.userProfile.registrationDate,
          status: this.userProfile.status,
          tenantId: this.userProfile.tenantId,
        };
        if (this.userProfile.image instanceof Uint8Array) {
          updatedUser.image = btoa(String.fromCharCode.apply(null, this.userProfile.image));
        } else if (typeof this.userProfile.image === 'string') {
          updatedUser.image = this.userProfile.image.split(',')[1]; // Remove data URL prefix if present
        }
        if (this.userType === "Passenger") {
          if (this.userProfile.discountId) {
            updatedUser.discountId = this.userProfile.discountId;
          }
          if (this.userProfile.discount) {
            updatedUser.discount = this.userProfile.discount;
          }
        }
        if(this.userType==="Passenger"){
          this.passengerService.updatePassenger(updatedUser).subscribe({
            next: () => {
              console.log('Profile updated successfully');
              const userData = localStorage.getItem('user');
              if (userData) {
              const userObject = JSON.parse(userData);
              userObject.email = this.profileForm.value.email;
              localStorage.setItem('user', JSON.stringify(userObject));
              }
              this.snackBar.open('Profile updated successfully!', 'Close', { duration: 3000 });
              this.loadUserProfile();
            },
            error: (error) => {
              console.error('Error updating profile:', error);
            }
          });
        }
        if (this.userType === "Manager") {
            updatedUser.hireDate = this.userProfile.hireDate;
            updatedUser.department = this.userProfile.department;
            updatedUser.managerLevel = this.userProfile.managerLevel;
        }
        if(this.userType==="Manager"){
          this.managerService.updateManager(updatedUser).subscribe({
            next: () => {
              console.log('Profile updated successfully');
              const userData = localStorage.getItem('user');
              if (userData) {
              const userObject = JSON.parse(userData);
              userObject.email = this.profileForm.value.email;
              localStorage.setItem('user', JSON.stringify(userObject));
              }
              this.snackBar.open('Profile updated successfully!', 'Close', { duration: 3000 });
              this.loadUserProfile();
            },
            error: (error) => {
              console.error('Error updating profile:', error);
            }
          });
        }
        if (this.userType === "Driver") {
            updatedUser.license = this.userProfile.license;
            updatedUser.driversLicenseNumber = this.userProfile.driversLicenseNumber;
            updatedUser.hireDate = this.userProfile.hireDate;
            updatedUser.workingHoursInAWeek = this.userProfile.workingHoursInAWeek;
        }
        if(this.userType==="Driver"){
          this.driverService.updateDriver( updatedUser).subscribe({
            next: () => {
              console.log('Profile updated successfully');
              const userData = localStorage.getItem('user');
              if (userData) {
              const userObject = JSON.parse(userData);
              userObject.email = this.profileForm.value.email;
              localStorage.setItem('user', JSON.stringify(userObject));
              }

              this.snackBar.open('Profile updated successfully!', 'Close', { duration: 3000 });
              this.loadUserProfile();
            },
            error: (error) => {
              console.error('Error updating profile:', error);
            }
          });
        }
      }
    }else {
      // Mark all fields as touched to show validation errors
      Object.keys(this.profileForm.controls).forEach(key => {
        const control = this.profileForm.get(key);
        control?.markAsTouched();
      });
      this.snackBar.open('Please correct the errors in the form.', 'Close', { duration: 3000 });
    }
  }

  getUserType(user: any): string {
    if ('department' in user) return 'Manager';
    if ('license' in user) return 'Driver';
    if ('discountID' in user) return 'Passenger';
    return 'Unknown';
  }

  onFileSelected(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e: ProgressEvent<FileReader>) => {
        const arrayBuffer = e.target?.result as ArrayBuffer;
        this.userProfile.image = new Uint8Array(arrayBuffer);
        this.updateProfileImage();
      };
      reader.readAsArrayBuffer(file);
    }
  }
  showQrCode(qrCodeBlob: any) {
    const qrCode = this.decodeQrCode(qrCodeBlob); // Use the decode helper
    this.dialog.open(QrCodeDialogComponent, {
      data: { qrCode },
    });
  }

  decodeQrCode(qrCodeBlob: any): string {
    if (typeof qrCodeBlob === 'string') {
      return qrCodeBlob.startsWith('data:') 
        ? qrCodeBlob 
        : `data:image/png;base64,${qrCodeBlob}`;
    }
  
    if (qrCodeBlob instanceof Uint8Array) {
      const binary = Array.from(qrCodeBlob, byte => String.fromCharCode(byte)).join('');
      return `data:image/png;base64,${btoa(binary)}`;
    }
  
    return '';
  }
  

}
