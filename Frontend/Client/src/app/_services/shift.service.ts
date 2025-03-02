import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Shift } from '../_models/shift';
import { catchError, Observable } from 'rxjs';
export interface ShiftDto {
  id: number
  busId: number
  busNumber: string
  driverId: number
  driverName: string
  shiftDate: string
  shiftStartingTime: string
  shiftEndingTime: string
  status: "Ended" | "Current" | "Upcoming"
}

export interface ShiftDetailDto {
  id: number
  lineId: number
  lineName: string
  startTime: string
  endTime: string
}

export interface ShiftDetailsDto {
  shift: ShiftDto
  details: ShiftDetailDto[]
}

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


  getDriverShifts(driverId: number, fromDate?: Date, toDate?: Date): Observable<ShiftDto[]> {
    let params = new HttpParams().set("driverId", driverId.toString())

    if (fromDate) {
      params = params.set("fromDate", fromDate.toISOString())
    }

    if (toDate) {
      params = params.set("toDate", toDate.toISOString())
    }

    return this.http.get<ShiftDto[]>(`${this.baseUrl}/driver-shift`, { params })
  }

  getCurrentShifts(driverId: number): Observable<ShiftDto[]> {
    const params = new HttpParams().set("driverId", driverId.toString())
    return this.http.get<ShiftDto[]>(`${this.baseUrl}/current`, { params })
  }

  getUpcomingShifts(driverId: number): Observable<ShiftDto[]> {
    const params = new HttpParams().set("driverId", driverId.toString())
    return this.http.get<ShiftDto[]>(`${this.baseUrl}/upcoming`, { params })
  }

  getEndedShifts(driverId: number): Observable<ShiftDto[]> {
    const params = new HttpParams().set("driverId", driverId.toString())
    return this.http.get<ShiftDto[]>(`${this.baseUrl}/ended`, { params })
  }

  getShiftDetails(shiftId: number): Observable<ShiftDetailsDto> {
    return this.http.get<ShiftDetailsDto>(`${this.baseUrl}/details/${shiftId}`)
  }

  downloadShiftPdf(shiftId: number): Observable<Blob> {
    return this.http.get(`${this.baseUrl}/${shiftId}/pdf`, {
      responseType: "blob",
    })
  }
}
