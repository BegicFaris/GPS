import { Component, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { NotificationService } from '../../_services/notification.service';
import { Notification } from '../../_models/notification';
import { NotificationEditComponent } from '../notification-edit/notification-edit.component';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-notification-view',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './notification-view.component.html',
  styleUrl: './notification-view.component.css'
})
export class NotificationViewComponent {
  private notificationService = inject(NotificationService);
  private router = inject(Router);
  private titleService = inject(Title);
  private dialog = inject(MatDialog)
  notifications: Notification[] = [];


  ngOnInit() {
    this.titleService.setTitle("Notifications");
    this.loadNotifications();
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd && event.url === '/manager-dashboard/notifications') {
        this.loadNotifications();
      }
    });
  }
  loadNotifications() {
    this.notificationService.getAllNotifications().subscribe(
      (data) => {
        this.notifications = data; 
      },
    );
  }

  deleteNotificatiom(id: number) {
    if (confirm('Are you sure you want to delete this notification?')) {
      this.notificationService.deleteNotification(id).subscribe({
        next: response => {
          this.loadNotifications();
          console.log('Notification deleted successfully', response);
          this.cancel(); 
        },
        error: error => {
          console.error('Error deleting notification', error);
        }
      });
    }
  }

  openEditDialog(notification: Notification) {
    const dialogRef = this.dialog.open(NotificationEditComponent, {
      height: '800px',
      width: '1000px',  
      data: {
        id:notification.id,
        description:notification.description,
        notificationTypeId:notification.notificationTypeId,
        duration:notification.duration,
        date:notification.date,
        isActive:notification.isActive,
        lineId:notification.lineId
      },  
    });
    dialogRef.afterClosed().subscribe(result => {
      this.loadNotifications();
      if (result) {
        console.log('Updated Notification:', result);
      }
    });

  }
  cancel() {
    this.router.navigate(['/manager-dashboard/notifications']);
  }
}



