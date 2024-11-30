import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Schedule } from '../_models/schedule';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/schedules';
  schedules: Schedule[] = [];

  getAllSchedules() {
    return this.http.get<Schedule[]>(this.baseUrl);
  }

  getSchedule(id: number) {
    return this.http.get<Schedule>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching schedule:', error);
        return (error);
      })
    );
  }

  createSchedule(schedule: any) {
    return this.http.post<Schedule>(this.baseUrl, schedule).pipe(
      catchError(error => {
        console.error('Error creating schedule:', error);
        return (error);
      })
    );
  }

  updateSchedule(schedule: any) {
    return this.http.put<Schedule>(this.baseUrl + `/${schedule.id}`, schedule).pipe(
      catchError(error => {
        console.error('Error updating schedule:', error);
        return (error);
      })
    );
  }

  deleteSchedule(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting schedule:', error);
        return (error);
      })
    );
  }
}
