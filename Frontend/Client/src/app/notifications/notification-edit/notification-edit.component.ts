import { Component, ElementRef, inject, Inject, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LineService } from '../../_services/line.service';
import { NotificationService } from '../../_services/notification.service';
import { NotificationTypeService } from '../../_services/notification-type.service';
import { NotificationType } from '../../_models/notification-type';
import { Title } from '@angular/platform-browser';
import { Line } from '../../_models/line';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-notification-edit',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './notification-edit.component.html',
  styleUrl: './notification-edit.component.css'
})
export class NotificationEditComponent {
  @ViewChild('fileInput') fileInput!: ElementRef;
  fileName: string = '';
  imagePreview: string | ArrayBuffer | null = null;
  constructor(public dialogRef: MatDialogRef<NotificationEditComponent>, @Inject(MAT_DIALOG_DATA) public notificationUpdate:
    {
      id: number,
      title: string,
      description: string,
      image: string,
      notificationTypeId: number,
      creationDate: string,
      lineId: number
    }) {}

  private notificationService = inject(NotificationService);
  private lineService = inject(LineService);
  private notificationTypeService = inject(NotificationTypeService);
  private titleService = inject(Title);

  lines: Line[] = [];
  notificationTypes: NotificationType[] = [];

  ngOnInit(): void {
    this.titleService.setTitle("Update notification");
    this.loadLines();
    this.loadNotificationTypes();
    if (this.notificationUpdate.image) {
      this.imagePreview = `data:image/jpeg;base64,${this.notificationUpdate.image}`;
    }
  }

  saveChanges(updateNotificationForm: NgForm) {
    if (updateNotificationForm.valid) {
      this.notificationService.updateNotification(this.notificationUpdate).subscribe({
        next: response => {
          console.log('Notification updated successfully', response);
        }
      });
      this.dialogRef.close(this.notificationUpdate);
    }
  }

  closeDialog() {
    this.dialogRef.close();
  }

  loadLines() {
    this.lineService.getAllLines().subscribe(
      (data) => {
        this.lines = data;
      },
    );
  }

  loadNotificationTypes() {
    this.notificationTypeService.getAllNotificationTypes().subscribe(
      (data) => {
        this.notificationTypes = data;
      },
    );
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
        this.fileInput.nativeElement=this.notificationUpdate.image; // Automatically clear the file input
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
          this.notificationUpdate.image = base64Image; // Assign base64 image to model
        }
        
        // Store the full base64 image for previewing (the complete data URL, including header)
        this.imagePreview = reader.result as string; // Directly use the full result for preview
        console.log(this.notificationUpdate.image);
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
          this.notificationUpdate.image = base64Image;
        }
      };
      reader.readAsDataURL(file); // Read the file as base64
    } else {
      alert('Please drop a valid image file.');
    }
  }

  
}
