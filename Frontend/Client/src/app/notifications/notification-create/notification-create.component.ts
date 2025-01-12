import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Line } from '../../_models/line';
import { NotificationType } from '../../_models/notification-type';
import { Title } from '@angular/platform-browser';
import { LineService } from '../../_services/line.service';
import { NotificationTypeService } from '../../_services/notification-type.service';
import { FormsModule, NgForm } from '@angular/forms';
import { NotificationService } from '../../_services/notification.service';
import { AccountService } from '../../_services/account.service';
import { MyAppUserService } from '../../_services/my-app-user.service';
import { formatDate } from '@angular/common';
import { firstValueFrom } from 'rxjs';


@Component({
  selector: 'app-notification-create',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './notification-create.component.html',
  styleUrl: './notification-create.component.css'
})
export class NotificationCreateComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;
  imagePreview: string | ArrayBuffer | null = null;
  fileName: string = ''

  private router = inject(Router);
  private lineService = inject(LineService);
  private notificationTypeService = inject(NotificationTypeService)
  private notificationService = inject(NotificationService);
  private titleService = inject(Title);
  private accountService = inject(AccountService);
  private appUserService = inject(MyAppUserService);
  lines: Line[] = [];
  notificationTypes: NotificationType[] = [];
  notificationCreate: any = {};

 async ngOnInit() {
    this.titleService.setTitle('Add notification');
    this.loadLines();
    this.loadNotificationTypes();
    await this.setManagerId();
  }
  async setManagerId() {
    const currentEmail = this.accountService.currentUser()?.email;
    console.log("Current email: " + currentEmail);
  
    if (currentEmail) {
      try {
        const data = await firstValueFrom(this.appUserService.getMyAppUserByEmail(currentEmail));
        this.notificationCreate.managerId = data.id;
      } catch (error) {
        console.error('Error fetching manager data:', error);
      }
    } 
  }
  addNewNotification(newNotificationForm: NgForm) {
    if (newNotificationForm.valid) {
      if (this.notificationCreate.lineId == "")
        this.notificationCreate.lineId = null;
      this.notificationCreate.creationDate = formatDate(new Date(), 'yyyy-MM-dd', 'en');

      console.log(this.notificationCreate);
      this.notificationService.createNotification(this.notificationCreate).subscribe({
        next: (response) => {
          console.log(response);
          this.cancel();
        },
      });
      this.router.navigate(['/manager-dashboard/notifications']);
    }
    else {
      console.log("Not valid");
      Object.keys(newNotificationForm.controls).forEach(field => {
        const control = newNotificationForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
    }
  }
  onImageClick(): void {
    this.fileInput.nativeElement.click();
  }

 onFileSelected(event: Event): void {
  const input = event.target as HTMLInputElement;

  if (input.files && input.files[0]) {
    const file = input.files[0];
    const validImageTypes = ['image/jpeg', 'image/png']; // Add allowed types

    // Check if the file type is valid
    if (!validImageTypes.includes(file.type)) {
      alert('Please upload a valid image file (JPEG, PNG, or GIF).');
      this.clearFileInput(); // Automatically clear the file input
      return;
    }

    // Set the file name to the file input
    this.fileName = file.name;

    // Create a FileReader to read the image file
    const reader = new FileReader();

    reader.onload = () => {
      // Ensure the base64 string is stored in notificationCreate.image
      const base64Image = reader.result?.toString().split(',')[1]; // Extract base64 part

      if (base64Image) {
        this.notificationCreate.image = base64Image; // Assign base64 image to model
      }
      
      // Store the full base64 image for previewing (the complete data URL, including header)
      this.imagePreview = reader.result as string; // Directly use the full result for preview
      console.log(this.notificationCreate.image);
    };

    // Read the image file as base64
    reader.readAsDataURL(file);
  }
}

onDragOver(event: DragEvent): void {
  event.preventDefault(); // Necessary for drag events
}

// Handle the drop event to process the dropped file
onDrop(event: DragEvent): void {
  event.preventDefault(); // Prevent default behavior (Prevent opening the file)

  const file = event.dataTransfer?.files[0]; // Get the first dropped file

  if (file && file.type.startsWith('image/')) {
    // Set the file name to the file input
    this.fileName = file.name;

    const reader = new FileReader();
    reader.onload = () => {
      // Set the imagePreview to the base64 result
      this.imagePreview = reader.result?.toString() || '';

      // Set notificationCreate.image with the base64 image part
      const base64Image = reader.result?.toString().split(',')[1]; // Extract the base64 part
      if (base64Image) {
        this.notificationCreate.image = base64Image;
      }
    };
    reader.readAsDataURL(file); // Read the file as base64
  } else {
    alert('Please drop a valid image file.');
  }
}
  clearFileInput(): void {
    this.fileInput.nativeElement.value = '';
    this.notificationCreate.image = null;
  }



  cancel() {
    this.router.navigate(['/manager-dashboard/notifications']);
  }
  loadLines() {
    this.lineService.getAllLines().subscribe((data) => {
      this.lines = data;
    });
  }
  loadNotificationTypes() {
    this.notificationTypeService.getAllNotificationTypes().subscribe((data) => {
      this.notificationTypes = data; // or data.lines if it's nested
    });
  }

}
