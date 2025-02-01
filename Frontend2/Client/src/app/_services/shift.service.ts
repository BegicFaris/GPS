import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Shift } from '../_models/shift';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShiftService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/shifts';
  shifts: Shift[] = [];

  getAllShifts() {
    return this.http.get<Shift[]>(this.baseUrl);
  }

  getShift(id: number) {
    return this.http.get<Shift>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching shift:', error);
        return (error);
      })
    );
  }

  createShift(shift: any) {
    return this.http.post<Shift>(this.baseUrl, shift).pipe(
      catchError(error => {
        console.error('Error creating shift:', error);
        return (error);
      })
    );
  }

  updateShift(shift: any) {
    return this.http.put<Shift>(this.baseUrl + `/${shift.id}`, shift).pipe(
      catchError(error => {
        console.error('Error updating shift:', error);
        return (error);
      })
    );
  }

  deleteShift(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting shift:', error);
        return (error);
      })
    );
  }
}
