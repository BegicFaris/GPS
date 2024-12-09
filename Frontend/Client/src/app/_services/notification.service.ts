import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Notification } from '../_models/notification';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/notifications';
  notifications: Notification[] = [];

  getAllNotifications() {
    return this.http.get<Notification[]>(this.baseUrl);
  }

  getNotification(id: number) {
    return this.http.get<Notification>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching notification:', error);
        return (error);
      })
    );
  }

  createNotification(notification: any) {
    return this.http.post<Notification>(this.baseUrl, notification).pipe(
      catchError(error => {
        console.error('Error creating notification:', error);
        return (error);
      })
    );
  }

  updateNotification(notification: any) {
    return this.http.put<Notification>(this.baseUrl + `/${notification.id}`, notification).pipe(
      catchError(error => {
        console.error('Error updating notification:', error);
        return (error);
      })
    );
  }

  deleteNotification(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting notification:', error);
        return (error);
      })
    );
  }
}
