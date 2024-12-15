import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { Line } from '../../_models/line';
import { NotificationType } from '../../_models/notification-type';
import { Title } from '@angular/platform-browser';
import { LineService } from '../../_services/line.service';
import { NotificationTypeService } from '../../_services/notification-type.service';
import { FormsModule, NgForm } from '@angular/forms';
import { NotificationService } from '../../_services/notification.service';


@Component({
  selector: 'app-notification-create',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './notification-create.component.html',
  styleUrl: './notification-create.component.css'
})
export class NotificationCreateComponent {
  private router = inject(Router);
  private lineService = inject(LineService);
  private notificationTypeService=inject(NotificationTypeService)
  private notificationService=inject(NotificationService);
  private titleService = inject(Title);
  lines: Line[] = [];
  notificationTypes: NotificationType[] = [];
  notificationCreate : any={};

  ngOnInit() {
    this.titleService.setTitle('Add notification');
    this.loadLines();
    this.loadNotificationTypes();
  }
  addNewNotification(newNotificationForm: NgForm) {

    if (newNotificationForm.valid) {
      console.log(this.notificationCreate);
      if (this.notificationCreate.isActive === undefined) {
        this.notificationCreate.isActive = false;
      }
      this.notificationService.createNotification(this.notificationCreate).subscribe({
        next: (response) => {
          console.log(response);
          this.cancel();
        },
      });
      this.router.navigate(['/manager-dashboard/notifications']);
    }
    else{
      Object.keys(newNotificationForm.controls).forEach(field => {
        const control = newNotificationForm.controls[field];
        control.markAsTouched({ onlySelf: true });
      });
    }
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
