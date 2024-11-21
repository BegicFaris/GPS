// src/app/bus.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bus } from './models/bus.model';

@Injectable({
  providedIn: 'root'
})
export class BusService {
  private apiUrl = 'https://localhost:5001/api/buses'; // Ensure this is the correct API URL

  constructor(private http: HttpClient) { }

  // Get all buses
  getAllBuses(): Observable<Bus[]> {
    return this.http.get<Bus[]>(this.apiUrl);
  }

  // Get a specific bus by ID
  getBusById(id: number): Observable<Bus> {
    return this.http.get<Bus>(`${this.apiUrl}/${id}`);
  }

  // Create a new bus
  createBus(bus: Bus): Observable<Bus> {
    return this.http.post<Bus>(this.apiUrl, bus);
  }

  // Update a bus
  updateBus(bus: Bus): Observable<Bus> {
    return this.http.put<Bus>(`${this.apiUrl}/${bus.id}`, bus);
  }

  // Delete a bus
  deleteBus(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
