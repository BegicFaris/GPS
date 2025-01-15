import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Notification } from '../_models/notification';
import { catchError, map, Observable } from 'rxjs';

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

  getNotifications(page: number, pageSize: number = 7): Observable<{
    items: Notification[],
    totalCount: number
  }> {
    return this.http.get<any>(`${this.baseUrl}/paged?page=${page}&pageSize=${pageSize}`);
  }


  createNotification(notification: any) {
    return this.http.post<Notification>(this.baseUrl, notification).pipe(
      catchError(error => {
        console.error('Error creating notification:', error);
        return (error);
      })
    );
  }

  getRecentNotifications(hours: number = 48): Observable<Notification[]> {
    return this.http.get<Notification[]>(`${this.baseUrl}/recent?hours=${hours}`);
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
