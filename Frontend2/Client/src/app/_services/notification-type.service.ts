import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { NotificationType } from '../_models/notification-type';

@Injectable({
  providedIn: 'root'
})
export class NotificationTypeService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/notificationtypes';


  getAllNotificationTypes() {
    return this.http.get<NotificationType[]>(this.baseUrl);
  }

  getNotificationType(id: number) {
    return this.http.get<NotificationType>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching notification type:', error);
        return (error);
      })
    );
  }

}
