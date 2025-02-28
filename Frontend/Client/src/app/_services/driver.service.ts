import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Driver } from '../_models/driver';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/drivers';
  drivers: Driver[] = [];

  getAllDrivers() {
    return this.http.get<Driver[]>(this.baseUrl);
  }

  getDriver(id: number) {
    return this.http.get<Driver>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching driver:', error);
        return throwError(() => error);
      })
    );
  }

  createDriver(driver: any) {
    return this.http.post<Driver>(this.baseUrl, driver).pipe(
      catchError(error => {
        console.error('Error creating driver:', error);
        return throwError(() => error);
      })
    );
  }

  updateDriver(driver: any) {
    return this.http.put<Driver>(this.baseUrl + `/${driver.id}`, driver).pipe(
      catchError(error => {
        console.error('Error updating driver:', error);
        return throwError(() => error);
      })
    );
  }

  deleteDriver(id: number): Observable<any> {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting driver:', error);
        return throwError(() => error);
      })
    );
  }
}
