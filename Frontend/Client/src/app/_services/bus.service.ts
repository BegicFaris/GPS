import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Bus } from '../_models/bus';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BusService {
  private http = inject(HttpClient);
  private baseUrl = 'https://localhost:5001/api/buses';
  buses: Bus[] = [];

  getAllBuses() {
    return this.http.get<Bus[]>(this.baseUrl);
  }

  getBus(id: number) {
    return this.http.get<Bus>(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error fetching bus:', error);
        return (error);
      })
    );
  }

  createBus(bus: any) {
    bus.capacity=bus.capacity.toString();
    bus.manufactureYear=bus.manufactureYear.toString();
    return this.http.post<Bus>(this.baseUrl, bus).pipe(
      catchError(error => {
        console.error('Error creating bus:', error);
        return (error);
      })
    );
  }

  updateBus(bus: any) {
    bus.capacity=bus.capacity.toString();
    bus.manufactureYear=bus.manufactureYear.toString();
    return this.http.put<Bus>(this.baseUrl + `/${bus.id}`, bus).pipe(
      catchError(error => {
        console.error('Error updating bus:', error);
        return (error);
      })
    );
  }

  deleteBus(id: number) {
    return this.http.delete(this.baseUrl + `/${id}`).pipe(
      catchError(error => {
        console.error('Error deleting bus:', error);
        return (error);
      })
    );
  }
}
