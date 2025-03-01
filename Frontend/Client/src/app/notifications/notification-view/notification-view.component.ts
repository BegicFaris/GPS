import { Component, inject } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NavigationEnd, Router, RouterLink } from '@angular/router';
import { NotificationService } from '../../_services/notification.service';
import { Notification } from '../../_models/notification';
import { NotificationEditComponent } from '../notification-edit/notification-edit.component';
import { Title } from '@angular/platform-browser';
import { lastValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-notification-view',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './notification-view.component.html',
  styleUrl: './notification-view.component.css'
})
export class NotificationViewComponent {
  private notificationService = inject(NotificationService);
  private router = inject(Router);
  private titleService = inject(Title);
  private dialog = inject(MatDialog)
  notifications: Notification[] = [];

  sortColumn: string = '';
  sortAsc: boolean = true;

  async ngOnInit() {
    this.titleService.setTitle("Notifications");


    await this.loadNotifications();
    await lastValueFrom(this.router.events);
    this.router.events.subscribe(async (event) => {
      console.log
      if (event instanceof NavigationEnd && event.url === '/manager-dashboard/notifications') {
        await this.loadNotifications();
      }
    });
  }
  async loadNotifications() {
    try {
      this.notifications = await lastValueFrom(this.notificationService.getAllNotifications());
    }
    catch (err) {
      console.error(err);
    }
  }

  deleteNotificatiom(id: number) {
    if (confirm('Are you sure you want to delete this notification?')) {
      this.notificationService.deleteNotification(id).subscribe({
        next: async response => {
          await this.loadNotifications();
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
      height: '850px',
      width: '1000px',
      data: {
        id: notification.id,
        title: notification.title,
        description: notification.description,
        image: notification.image,
        notificationTypeId: notification.notificationTypeId,
        creationDate: notification.creationDate,
        lineId: notification.lineId,
        managerId: notification.managerId
      },
    });
    dialogRef.afterClosed().subscribe(async result => {
      await this.loadNotifications();
      if (result) {
        console.log('Updated Notification:', result);
      }
    });

  }
  cancel() {
    this.router.navigate(['/manager-dashboard/notifications']);
  }

  sortTable(column: keyof Notification) {
    if (this.sortColumn === column) {
      this.sortAsc = !this.sortAsc;
    } else {
      this.sortColumn = column;
      this.sortAsc = true;
    }

    this.notifications.sort((a, b) => {
      let valueA = a[column];
      let valueB = b[column];

      // Handle nested properties like notificationType.name
      if (column === 'notificationType' && a.notificationType && b.notificationType) {
        valueA = a.notificationType.name;
        valueB = b.notificationType.name;
      }

      if (column === 'line' && a.line && b.line) {
        valueA = a.line.name;
        valueB = b.line.name;
      }

      // Convert to lowercase if values are strings
      if (typeof valueA === 'string') valueA = valueA.toLowerCase();
      if (typeof valueB === 'string') valueB = valueB.toLowerCase();

      // Handle null or undefined values to avoid returning undefined
      if (valueA == null) return this.sortAsc ? -1 : 1;
      if (valueB == null) return this.sortAsc ? 1 : -1;

      return this.sortAsc ? (valueA > valueB ? 1 : valueA < valueB ? -1 : 0) : (valueA < valueB ? 1 : valueA > valueB ? -1 : 0);
    });
  }

  getSortIcon(column: keyof Notification) {
    if (this.sortColumn === column) {
      return this.sortAsc ? 'sort-asc' : 'sort-desc';
    }
    return '';
  }


}



