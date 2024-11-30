import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { catchError } from 'rxjs';
import { Passenger } from '../_models/passenger';

@Injectable({
  providedIn: 'root'
})
export class PassengerService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/passengers';
  passengers: Passenger[] = [];

  getAllPassengers() {
    return this.http.get<Passenger[]>(this.baseUrl);
  }

  getPassenger(id: number) {
    return this.http.get<Passenger>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching passenger:', error);
        return (error);
      })
    );
  }

  createPassenger(passenger: any) {
    return this.http.post<Passenger>(this.baseUrl, passenger).pipe(
      catchError(error => {
        console.error('Error creating passenger:', error);
        return (error);
      })
    );
  }

  updatePassenger(passenger: any) {
    return this.http.put<Passenger>(this.baseUrl + `/${passenger.id}`, passenger).pipe(
      catchError(error => {
        console.error('Error updating passenger:', error);
        return (error);
      })
    );
  }

  deletePassenger(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting passenger:', error);
        return (error);
      })
    );
  }
}
