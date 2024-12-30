import { Component, inject, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LineEditComponent } from '../../lines/line-edit/line-edit.component';
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
  constructor(public dialogRef: MatDialogRef<NotificationEditComponent>, @Inject(MAT_DIALOG_DATA) public notificationUpdate:
    {
      id: number,
      description: string,
      notificationTypeId: number,
      duration: string,
      date: string,
      lineId: number
      isActive: boolean
    }) {

  }
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
  }

  saveChanges(updateNotificationForm: NgForm) {
    if(updateNotificationForm.valid){
      this.notificationService.updateNotification(this.notificationUpdate).subscribe({
        next: response => {
          console.log('Notification update successfully', response);
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
        this.lines = data; // or data.lines if it's nested
      },
    );
  }
  loadNotificationTypes() {
    this.notificationTypeService.getAllNotificationTypes().subscribe(
      (data) => {
        this.notificationTypes = data; // or data.lines if it's nested
      },
    );
  }

}
